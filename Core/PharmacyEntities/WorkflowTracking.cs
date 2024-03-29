﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.PharmacyEntities
{
    public class WorkflowTracking :BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string AdminId { get; set; }
        public string? AdminName { get; set; }
        public string? ActionPerformed { get; set; }
        public ApplicationUser? Admin { get; set; }
        [Required]
        public string Details { get; set; } // => The action that the admin done
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
