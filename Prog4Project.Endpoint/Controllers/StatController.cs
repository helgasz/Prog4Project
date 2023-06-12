using Microsoft.AspNetCore.Mvc;
using Prog4Project.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Prog4Project.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IProjectLogic logic;

        public StatController(IProjectLogic logic)
        {
            this.logic = logic;
        }

        // GET: api/<StatController>
        [HttpGet]
        public double? Average(int mgt)
        {
            return this.logic.GetAvarageDifficulityPerManager(mgt);
        }

        // GET api/<StatController>/5
        [HttpGet]
        public double? GetManagerProjectNumber(int mgt)
        {
            return this.logic.GetManagerProjectNumber(mgt);
        }
    }
}
