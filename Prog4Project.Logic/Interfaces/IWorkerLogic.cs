using Prog4Project.Models;
using System.Linq;

namespace Prog4Project.Logic
{
    public interface IWorkerLogic
    {
        void Create(Worker item);
        void Delete(int id);
        Worker Read(int id);
        IQueryable<Worker> ReadAll();
        void Update(Worker item);
    }
}