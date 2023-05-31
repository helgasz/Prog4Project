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
    public class ManagerController : ControllerBase
    {
        IManagerLogic logic;

        public ManagerController(IManagerLogic logic)
        {
            this.logic = logic;
        }

        // GET: api/<ManagerController>
        [HttpGet]
        public IEnumerable<ProjectManager> ReadAll()
        {
            return this.logic.ReadAll();
        }

        // GET api/<ManagerController>/5
        [HttpGet("{id}")]
        public ProjectManager Read(int id)
        {
            return this.logic.Read(id);
        }

        // POST api/<ManagerController>
        [HttpPost]
        public void Create([FromBody] ProjectManager value)
        {
            this.logic.Create(value);
        }

        // PUT api/<ManagerController>/5
        [HttpPut("{id}")]
        public void Update([FromBody] ProjectManager value)
        {
            this.logic.Update(value);
        }

        // DELETE api/<ManagerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
