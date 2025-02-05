using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BX.Models
{
    public class UserGoogleID
    {
        [Key, ForeignKey("User")]
        public string UserId { get; set; }

        [Required]
        public string GoogleID { get; set; }

        public virtual User User { get; set; }
    }
}
