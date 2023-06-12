using Prog4Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog4Project.WPFClient
{
    public class _ManagerTry2
    {

        public RestCollection<ProjectManager> managers { get; set; }

        public _ManagerTry2()
        {
            managers = new RestCollection<ProjectManager>("http://localhost:20741/", "manager");
        }
    }
}
