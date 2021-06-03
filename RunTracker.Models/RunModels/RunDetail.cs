using RunTracker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTracker.Models
{
    public class RunDetail
    {
        public int Id { get; set; }

        public double DistanceInMiles { get; set; }

        public double DistanceInKm { get; set; }

        public TimeSpan Time { get; set; }

        public DateTime Date { get; set; }

        public string LocationName { get; set; }

        public TimeSpan AvgMilePace { get; set; }

        public TimeSpan AvgKmPace { get; set; }

        public double AvgSpeedMPH { get; set; }

        public double AvgSpeedKPH { get; set; }
    }
}
