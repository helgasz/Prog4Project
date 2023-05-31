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
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectId { get; set; }

        [StringLength(240)]
        public string ProjectName { get; set; }

        [Range(0, 10000)]
        public double Price { get; set; }

        [Range(0, 10)]
        public double Difficulity { get; set; }

        public DateTime Finished { get; set; }

        public int ManagerId { get; set; }

        public virtual ProjectManager Manager { get; set; }

        [JsonIgnore]
        public virtual ICollection<Worker> Workers { get; set; }

        public virtual ICollection<Position> Positions { get; set; }


        public Project()
        {

        }

        public Project(string variable)
        {
            string[] split = variable.Split('#');
            ProjectId = int.Parse(split[0]);
            ProjectName = split[1];
            Price = double.Parse(split[2]);
            ManagerId = int.Parse(split[3]);
            Finished = DateTime.Parse(split[4].Replace('*', '.'));
            Difficulity = double.Parse(split[5]);
        }
    }
}
