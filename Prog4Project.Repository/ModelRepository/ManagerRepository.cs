using Prog4Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog4Project.Repository
{
    public class ManagerRepository : Repository<ProjectManager>, IRepository<ProjectManager>
    {
        public ManagerRepository(ProjectDbContext ctx) : base(ctx)
        {
        }

        public override ProjectManager Read(int id)
        {
            return ctx.Managers.FirstOrDefault(t => t.ManagerId == id);
        }

        public override void Update(ProjectManager item)
        {
            var old = Read(item.ManagerId);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}
