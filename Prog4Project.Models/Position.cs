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
    public class Position
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PositionId { get; set; }

        public int Priority { get; set; }
        public string PositionName { get; set; }

        public int ProjectId { get; set; }
        public int WorkerId { get; set; }

        public virtual Worker Worker { get; private set; }

        [JsonIgnore]
        public virtual Project Project { get; private set; }

        public Position()
        {

        }

        public Position(string variable)
        {
            string[] split = variable.Split('#');
            PositionId = int.Parse(split[0]);
            ProjectId = int.Parse(split[1]);
            WorkerId = int.Parse(split[2]);
            Priority = int.Parse(split[3]);
            PositionName = split[4];
        }
    }
}
