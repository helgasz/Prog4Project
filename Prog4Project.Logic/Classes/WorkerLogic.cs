using Prog4Project.Models;
using Prog4Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog4Project.Logic
{
    public class WorkerLogic : IWorkerLogic
    {
        IRepository<Worker> repo;

        public WorkerLogic(IRepository<Worker> repo)
        {
            this.repo = repo;
        }

        public void Create(Worker item)
        {
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Worker Read(int id)
        {
            var worker = this.repo.Read(id);
            if (worker == null)
            {
                throw new ArgumentException("Worker not exist");
            }
            return worker;
        }

        public IQueryable<Worker> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Worker item)
        {
            this.repo.Update(item);
        }
    }
}
