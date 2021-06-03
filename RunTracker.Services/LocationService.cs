using RunTracker.Data;
using RunTracker.Models;
using RunTracker.Models.LocationModels;
using RunTracker.Models.RunModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTracker.Services
{
    public class LocationService
    {
        private readonly Guid _userId;

        public LocationService(Guid userId)
        {
            _userId = userId;
        }

        public LocationDetail GetLocationById(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var entity = context.Locations.SingleOrDefault(d => d.Id == id && d.UserId == _userId);

                if (entity == null)
                    return null;

                var distanceService = CreateDistanceService();
                var runs = new List<RunListItem_Location>();
                foreach (var run in entity.Runs)
                {
                    var listItem = new RunListItem_Location()
                    {
                        Id = run.Id,
                        Date = run.Date,
                        Time = run.Time,
                        DistanceAmt = distanceService.GetDistanceById(run.DistanceId).Amount
                    };
                    runs.Add(listItem);
                }

                return new LocationDetail()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Runs = runs
                };
            }
        }

        public IEnumerable<LocationListItem> GetLocations()
        {
            using (var context = new ApplicationDbContext())
            {
                var entities = context.Locations.Where(l => l.UserId == _userId)
                    .Select(e => new LocationListItem
                        {
                            Id = e.Id,
                            Name = e.Name
                        });
                return entities.ToArray();
            }
        }

        public bool CreateLocation(LocationCreate model)
        {
            using (var context = new ApplicationDbContext())
            {
                if (context.Locations.Any(l => l.Name == model.Name
                                            && l.UserId == _userId))
                    return true;

                var location = new Location()
                {
                    Name = model.Name,
                    UserId = _userId
                };
                context.Locations.Add(location);
                return context.SaveChanges() == 1;
            }
        }

        public bool CreateLocation(RunCreate model)
        {
            using (var context = new ApplicationDbContext())
            {
                if (context.Locations.Any(l => l.Name == model.LocationName
                                            && l.UserId == _userId))
                    return true;

                var location = new Location()
                {
                    Name = model.LocationName,
                    UserId = _userId
                };
                context.Locations.Add(location);
                return context.SaveChanges() == 1;
            }
        }

        public bool UpdateLocation(LocationEdit model)
        {
            using (var context = new ApplicationDbContext())
            {
                var entity = context.Locations.Single(e => e.Id == model.Id && e.UserId == _userId);

                entity.Name = model.Name;

                return context.SaveChanges() == 1;
            }
        }

        public bool DeleteLocation(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var entity = context.Locations.Single(r => r.Id == id && r.UserId == _userId);

                context.Locations.Remove(entity);
                return context.SaveChanges() == 1;
            }
        }

        private DistanceService CreateDistanceService()
        {
            var distanceService = new DistanceService(_userId);
            return distanceService;
        }
    }
}
