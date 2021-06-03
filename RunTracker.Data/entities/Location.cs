﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTracker.Data
{
    public class Location
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Guid UserId { get; set; }

        // navigation properties
        public virtual ICollection<Run> Runs { get; set; } = new List<Run>();
    }
}
