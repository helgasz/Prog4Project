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
        public double? Average(int year)
        {
            return this.logic.GetAvarageDifficulityPerYear(year);
        }

        // GET api/<StatController>/5
        [HttpGet]
        public IEnumerable<ProjectLogic.YearInfo> YearStatistics(int year)
        {
            return this.logic.YearStatistics();
        }
    }
}
