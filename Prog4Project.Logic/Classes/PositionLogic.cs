using Prog4Project.Models;
using Prog4Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog4Project.Logic
{
    public class PositionLogic : IPositionLogic
    {
        IRepository<Position> repo;

        public PositionLogic(IRepository<Position> repo)
        {
            this.repo = repo;
        }

        public void Create(Position item)
        {
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Position Read(int id)
        {
            var position = this.repo.Read(id);
            if (position == null)
            {
                throw new ArgumentException("Position not exist");
            }
            return position;
        }

        public IQueryable<Position> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Position item)
        {
            this.repo.Update(item);
        }
    }
}
