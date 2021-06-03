using RunTracker.Data;
using RunTracker.Models;
using RunTracker.Models.DistanceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTracker.Services
{
    public class DistanceService
    {
        private readonly Guid _userId;

        public DistanceService(Guid userId)
        {
            _userId = userId;
        }

        public DistanceDetail GetDistanceById(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var entity = context.Distances.SingleOrDefault(d => d.Id == id && d.UserId == _userId);
                string amount;
                if (entity == null)
                    return null;

                if (entity.IsMiles)
                {
                    if (entity.Amount >= 3.1 && entity.Amount <= 3.11)
                        amount = $"{entity.Amount}m (5k)";
                    else if (entity.Amount == 6.2)
                        amount = $"{entity.Amount}m (10k)";
                    else if (entity.Amount >= 9.3 && entity.Amount <= 9.4)
                        amount = $"{entity.Amount}m (15k)";
                    else if (entity.Amount >= 12 && entity.Amount <= 12.5)
                        amount = $"{entity.Amount}m (20k)";
                    else if (entity.Amount == 13.1)
                        amount = $"{entity.Amount}m (Half marathon)";
                    else if (entity.Amount >= 15.5 && entity.Amount <= 16)
                        amount = $"{entity.Amount}m (25k)";
                    else if (entity.Amount >= 18.6 && entity.Amount <= 19)
                        amount = $"{entity.Amount}m (30k)";
                    else if (entity.Amount == 26.2)
                        amount = $"{entity.Amount}m (Marathon)";
                    else
                        amount = $"{entity.Amount}m";
                }
                else
                    amount = $"{entity.Amount}km";

                var runs = new List<RunListItem_Distance>();
                foreach (var run in entity.Runs)
                {
                    var listItem = new RunListItem_Distance()
                    {
                        Id = run.Id,
                        Date = run.Date,
                        Time = run.Time,
                        LocationName = run.Location.Name
                    };
                    runs.Add(listItem);
                }

                return new DistanceDetail()
                {
                    Id = entity.Id,
                    Amount = amount,
                    Runs = runs
                };
            }
        }

        public IEnumerable<DistanceListItem> GetDistances()
        {
            using (var context = new ApplicationDbContext())
            {
                var entities = context.Distances.Where(d => d.UserId == _userId).ToList();

                var distanceList = new List<DistanceListItem>();
                foreach (var entity in entities)
                {
                    var detailModel = GetDistanceById(entity.Id);
                    var listItem = new DistanceListItem()
                    {
                        Id = detailModel.Id,
                        Amount = detailModel.Amount
                    };
                    distanceList.Add(listItem);
                }
                return distanceList.OrderBy(d => d.Amount);
            }
        }

        public bool CreateDistance(DistanceCreate model)
        {
            using (var context = new ApplicationDbContext())
            {
                if (context.Distances.Any(d => d.Amount == model.Amount
                                            && d.IsMiles == model.IsMiles
                                            && d.UserId == _userId))
                    return true;

                var distance = new Distance()
                {
                    Amount = model.Amount,
                    IsMiles = model.IsMiles,
                    UserId = _userId
                };
                context.Distances.Add(distance);
                return context.SaveChanges() == 1;
            }
        }

        public bool CreateDistance(RunCreate model)
        {
            using (var context = new ApplicationDbContext())
            {
                if (context.Distances.Any(d => d.Amount == model.Distance
                                            && d.IsMiles == model.IsMiles
                                            && d.UserId == _userId))
                    return true;

                var distance = new Distance()
                {
                    Amount = model.Distance,
                    IsMiles = model.IsMiles,
                    UserId = _userId
                };
                context.Distances.Add(distance);
                return context.SaveChanges() == 1;
            }
        }

        public bool UpdateDistance(DistanceEdit model)
        {
            using (var context = new ApplicationDbContext())
            {
                var entity = context.Distances.Single(e => e.Id == model.Id && e.UserId == _userId);

                entity.Amount = model.Amount;
                entity.IsMiles = model.IsMiles;

                return context.SaveChanges() == 1;
            }
        }

        public bool DeleteDistance(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var entity = context.Distances.Single(r => r.Id == id && r.UserId == _userId);

                context.Distances.Remove(entity);
                return context.SaveChanges() == 1;
            }
        }
    }
}
