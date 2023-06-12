using Prog4Project.Models;
using System.Collections.Generic;
using System.Linq;

namespace Prog4Project.Logic
{
    public interface IProjectLogic
    {
        void Create(Project item);
        void Delete(int id);
        double? GetAvarageDifficulityPerManager(int year);
        Project Read(int id);
        IQueryable<Project> ReadAll();
        void Update(Project item);
        IEnumerable<ProjectLogic.ManagerInfo> ManagerStat();
    }
}