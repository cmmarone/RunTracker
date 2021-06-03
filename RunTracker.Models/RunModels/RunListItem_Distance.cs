using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTracker.Models.DistanceModels
{
    public class RunListItem_Distance
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }

        public string LocationName { get; set; }
    }
}
