using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.PharmacyEntities
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime HiringDate { get; set; } = DateTime.Now;
        public ApplicationUser? Supervisor { get; set; }
        public string? SupervisorId { get; set; }
    }
}
