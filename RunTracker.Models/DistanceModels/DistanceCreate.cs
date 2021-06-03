using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTracker.Models.DistanceModels
{
    public class DistanceCreate
    {
        [Required]
        public double Amount { get; set; }

        [Required]
        public bool IsMiles { get; set; }
    }
}
