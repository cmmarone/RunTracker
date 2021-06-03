using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTracker.Models.RunModels
{
    public class RunListItem_Location
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }

        public string DistanceAmt { get; set; }
    }
}
