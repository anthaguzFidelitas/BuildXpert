using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BX.Models
{
    public class ProjectState
    {
        public string ProjectStateId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
