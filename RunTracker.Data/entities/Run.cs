using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTracker.Data
{
    public class Run
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Distance))]
        public int DistanceId { get; set; }

        [Required]
        public TimeSpan Time { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [ForeignKey(nameof(Location))]
        public int LocationId { get; set; }

        public Guid UserId { get; set; }

        // nTavigation properties
        public virtual Distance Distance { get; set; }
        public virtual Location Location { get; set; }
    }
}
