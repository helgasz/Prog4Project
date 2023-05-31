using Prog4Project.Models;
using System.Linq;

namespace Prog4Project.Logic
{
    public interface IManagerLogic
    {
        void Create(ProjectManager item);
        void Delete(int id);
        ProjectManager Read(int id);
        IQueryable<ProjectManager> ReadAll();
        void Update(ProjectManager item);
    }
}