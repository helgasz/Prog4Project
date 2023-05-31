using Microsoft.AspNetCore.Mvc;
using Prog4Project.Logic;
using Prog4Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Prog4Project.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        IProjectLogic logic;

        public ProjectController(IProjectLogic logic)
        {
            this.logic = logic;
        }


        // GET: api/<ProjectController>
        [HttpGet]
        public IEnumerable<Project> ReadAll()
        {
            return this.logic.ReadAll();
        }

        // GET api/<ProjectController>/5
        [HttpGet("{id}")]
        public Project Read(int id)
        {
            return this.logic.Read(id);
        }

        // POST api/<ProjectController>
        [HttpPost]
        public void Create([FromBody] Project value)
        {
            this.logic.Create(value);
        }

        // PUT api/<ProjectController>/5
        [HttpPut("{id}")]
        public void Update([FromBody] Project value)
        {
            this.logic.Update(value);
        }

        // DELETE api/<ProjectController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
