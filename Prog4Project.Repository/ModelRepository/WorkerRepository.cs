using Prog4Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog4Project.Repository
{
    public class WorkerRepository : Repository<Worker>, IRepository<Worker>
    {
        public WorkerRepository(ProjectDbContext ctx) : base(ctx)
        {
        }

        public override Worker Read(int id)
        {
            return ctx.Workers.FirstOrDefault(t => t.WorkerId == id);
        }

        public override void Update(Worker item)
        {
            var old = Read(item.WorkerId);
            if (old == null)
            {
                throw new ArgumentException("Item not exist..");
            }
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }
            ctx.SaveChanges();
        }
    }
}
