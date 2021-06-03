using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTracker.Models
{
    public class RunEdit
    {
        public int Id { get; set; }

        public int DistanceId { get; set; }

        public TimeSpan Time { get; set; }

        public DateTime Date { get; set; }

        public int LocationId { get; set; }
    }
}
