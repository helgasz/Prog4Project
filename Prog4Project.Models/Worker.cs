using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Prog4Project.Models
{
    public class Worker
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WorkerId { get; set; }

        [Required]
        [StringLength(200)]
        public string WorkerName { get; set; }

        [JsonIgnore]
        public virtual ICollection<Project> Projects { get; set; }

        [JsonIgnore]
        public virtual ICollection<Position> Positions { get; set; }

        public Worker()
        { }

        public Worker(string variable)
        {
            string[] split = variable.Split('#');
            WorkerId = int.Parse(split[0]);
            WorkerName = split[1];
        }
    }
}
