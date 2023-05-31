using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Prog4Project.Models
{
    public class ProjectManager
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ManagerId { get; set; }

        [Required]
        [StringLength(200)]
        public string ManagerName { get; set; }

        [JsonIgnore]
        public virtual ICollection<Project> Projects { get; set; }

        public ProjectManager()
        {
            Projects = new HashSet<Project>();
        }

        public ProjectManager(string variable)
        {
            string[] split = variable.Split('#');
            ManagerId = int.Parse(split[0]);
            ManagerName = split[1];
            Projects = new HashSet<Project>();
        }

    }
}
