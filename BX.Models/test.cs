using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BX.Models
{
    public class test
    {
        [Key]
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public virtual ICollection<UserEmail> Emails { get; set; }
        public virtual UserGoogleID UserGoogleID { get; set; }
        public virtual ICollection<UserPhone> Phones { get; set; }
    }
}
