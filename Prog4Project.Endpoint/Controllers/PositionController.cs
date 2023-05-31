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
    public class PositionController : ControllerBase
    {
        IPositionLogic logic;

        public PositionController(IPositionLogic logic)
        {
            this.logic = logic;
        }

        // GET: api/<PositionController>
        [HttpGet]
        public IEnumerable<Position> ReadAll()
        {
            return this.logic.ReadAll();
        }

        // GET api/<PositionController>/5
        [HttpGet("{id}")]
        public Position Read(int id)
        {
            return this.logic.Read(id);
        }

        // POST api/<PositionController>
        [HttpPost]
        public void Create([FromBody] Position value)
        {
            this.logic.Create(value);
        }

        // PUT api/<PositionController>/5
        [HttpPut("{id}")]
        public void Update([FromBody] Position value)
        {
            this.logic.Update(value);
        }

        // DELETE api/<PositionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
