using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BX.Models
{
    public class User : IdentityUser
    {
        [Key]
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string GoogleID { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        //public virtual ICollection<UserEmail> Emails { get; set; }
        //public virtual UserGoogleID UserGoogleID { get; set; }
        public virtual ICollection<UserPhone> Phones { get; set; }
    }
}
