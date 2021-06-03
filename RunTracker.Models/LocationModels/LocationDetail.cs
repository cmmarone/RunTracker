using RunTracker.Models.RunModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTracker.Models.LocationModels
{
    public class LocationDetail
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<RunListItem_Location> Runs { get; set; }
    }
}
