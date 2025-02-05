using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BX.Models
{
    public class Project
    {
        [Key]
        public string ProjectID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public decimal Budget { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        [ForeignKey("ProjectOwner")]
        public string ProjectOwnerId { get; set; }
        public virtual User ProjectOwner { get; set; }

        [ForeignKey("Customer")]
        public string CustomerId { get; set; }
        public virtual User Customer { get; set; }

        [ForeignKey("ProjectState")]
        public string ProjectStateId { get; set; }
        public virtual ProjectState ProjectState { get; set; }
    }

}
