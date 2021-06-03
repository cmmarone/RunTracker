using RunTracker.Data;
using RunTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTracker.Services
{
    public class RunService
    {
        private readonly Guid _userId;

        public RunService(Guid userId)
        {
            _userId = userId;
        }

        public RunDetail GetRunById(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var entity = context.Runs.SingleOrDefault(e => e.Id == id && e.UserId == _userId);

                if (entity == null)
                    return null;

                var amount = entity.Distance.Amount;
                double distanceInMiles = (entity.Distance.IsMiles) ? amount : amount * 0.62137119223733;
                double distanceInKm = (entity.Distance.IsMiles) ? amount * 1.609344 : amount;
                double roundedDistanceInMiles = Math.Round(distanceInMiles, 2);
                double roundedDistanceInKm = Math.Round(distanceInKm, 2);

                double timeInMinutes = entity.Time.TotalSeconds / 60;
                var milePaceDbl = timeInMinutes / distanceInMiles;
                var milePaceSeconds = Math.Round(milePaceDbl * 60);
                var avgMilePace = TimeSpan.FromSeconds(milePaceSeconds);
                var kmPaceDbl = timeInMinutes / distanceInKm;
                var kmPaceSeconds = Math.Round(kmPaceDbl * 60);
                var avgKmPace = TimeSpan.FromSeconds(kmPaceSeconds);

                double timeInHours = entity.Time.TotalSeconds / 3600;
                double avgSpeedMPH = Math.Round((distanceInMiles / timeInHours), 1);
                double avgSpeedKPH = Math.Round((distanceInKm / timeInHours), 1);

                return new RunDetail
                {
                    Id = entity.Id,
                    DistanceInMiles = roundedDistanceInMiles,
                    DistanceInKm = roundedDistanceInKm,
                    Time = entity.Time,
                    Date = entity.Date,
                    LocationName = entity.Location.Name,
                    AvgMilePace = avgMilePace,
                    AvgKmPace = avgKmPace,
                    AvgSpeedMPH = avgSpeedMPH,
                    AvgSpeedKPH = avgSpeedKPH
                };
            }
        }

        public IEnumerable<RunDetail> GetRuns()
        {
            using (var context = new ApplicationDbContext())
            {
                var entities = context.Runs.Where(e => e.UserId == _userId).ToList();

                var runList = new List<RunDetail>();
                foreach (var entity in entities)
                {
                    var detail = GetRunById(entity.Id);
                    runList.Add(detail);
                }
                return runList.OrderByDescending(r => r.Date);
            }
        }

        public bool CreateRun(RunCreate model)
        {
            var distanceService = new DistanceService(_userId);
            if (!distanceService.CreateDistance(model))
                return false;

            var locationService = new LocationService(_userId);
            if (!locationService.CreateLocation(model))
                return false;

            var timeSpan = new TimeSpan(model.Hours, model.Minutes, model.Seconds);

            using (var context = new ApplicationDbContext())
            {
                var run = new Run()
                {
                    DistanceId = context.Distances
                        .SingleOrDefault(d => d.Amount == model.Distance
                        && d.IsMiles == model.IsMiles
                        && d.UserId == _userId).Id,
                    Time = timeSpan,
                    Date = model.Date,
                    LocationId = context.Locations
                        .SingleOrDefault(l => l.Name == model.LocationName
                        && l.UserId == _userId).Id,
                    UserId = _userId
                };
                context.Runs.Add(run);
                return context.SaveChanges() == 1;
            }
        }

        public bool UpdateRun(RunEdit model)
        {
            using (var context = new ApplicationDbContext())
            {
                var entity = context.Runs.Single(r => r.Id == model.Id && r.UserId == _userId);

                entity.DistanceId = model.DistanceId;
                entity.Time = model.Time;
                entity.Date = model.Date;
                entity.LocationId = model.LocationId;

                return context.SaveChanges() == 1;
            }
        }

        public bool DeleteRun(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var entity = context.Runs.Single(r => r.Id == id && r.UserId == _userId);

                context.Runs.Remove(entity);
                return context.SaveChanges() == 1;
            }
        }
    }
}
