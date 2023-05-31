using Prog4Project.Models;
using Prog4Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog4Project.Logic
{
    public class ManagerLogic : IManagerLogic
    {
        IRepository<ProjectManager> repo;

        public ManagerLogic(IRepository<ProjectManager> repo)
        {
            this.repo = repo;
        }

        public void Create(ProjectManager item)
        {
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public ProjectManager Read(int id)
        {
            var manager = this.repo.Read(id);
            if (manager == null)
            {
                throw new ArgumentException("Manager not exist");
            }
            return manager;
        }

        public IQueryable<ProjectManager> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(ProjectManager item)
        {
            this.repo.Update(item);
        }
    }
}
