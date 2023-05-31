using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
    public class WorkerController : ControllerBase
    {

        IWorkerLogic logic;

        public WorkerController(IWorkerLogic logic)
        {
            this.logic = logic;
        }

        // GET: api/<WorkerController>
        [HttpGet]
        public IEnumerable<Worker> ReadAll()
        {
            return this.logic.ReadAll();
        }

        // GET api/<WorkerController>/5
        [HttpGet("{id}")]
        public Worker Read(int id)
        {
            return this.logic.Read(id);
        }

        // POST api/<WorkerController>
        [HttpPost]
        public void Create([FromBody] Worker value)
        {
            this.logic.Create(value);
        }

        // PUT api/<WorkerController>/5
        [HttpPut]
        public void Put([FromBody] Worker value)
        {
            this.logic.Update(value);
        }

        // DELETE api/<WorkerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
