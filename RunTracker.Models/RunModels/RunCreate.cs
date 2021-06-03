using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTracker.Models
{
    public class RunCreate
    {
        [Required]
        public double Distance { get; set; }

        [Required]
        public bool IsMiles { get; set; }
        
        [Required]
        public int Hours { get; set; }

        [Required]
        public int Minutes { get; set; }

        [Required]
        public int Seconds { get; set; }

        /*  // alternate input - user can provide formatted TimeSpan
        [Required]
        public TimeSpan Time { get; set; }
        */

        [Required]
        public DateTime Date { get; set; }

        /* // alternate input - take in parsed date components from user
        [Required]
        public int DateMonth { get; set; }

        [Required]
        public int DateDay { get; set; }

        [Required]
        public int DateYear { get; set; }
        */

        [Required]
        public string LocationName { get; set; }
    }
}
