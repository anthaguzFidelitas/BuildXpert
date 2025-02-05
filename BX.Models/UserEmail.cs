using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BX.Models
{
    public class UserEmail
    {
        [Key, Column(Order = 0)]
        [ForeignKey("User")]
        public string UserId { get; set; }

        [Key, Column(Order = 1)]
        public string Email { get; set; }

        // Navigation property
        public virtual User User { get; set; }
    }
}
