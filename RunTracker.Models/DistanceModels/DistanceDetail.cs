using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTracker.Models.DistanceModels
{
    public class DistanceDetail
    {
        public int Id { get; set; }

        public string Amount { get; set; }

        public ICollection<RunListItem_Distance> Runs { get; set; }
    }
}
