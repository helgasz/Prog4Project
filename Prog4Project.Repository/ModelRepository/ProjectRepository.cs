using Prog4Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog4Project.Repository
{
    public class ProjectRepository : Repository<Project>, IRepository<Project>
    {
        public ProjectRepository(ProjectDbContext ctx) : base(ctx)
        {
        }

        public override Project Read(int id)
        {
            return ctx.Projects.FirstOrDefault(t => t.ProjectId == id);
        }

        public override void Update(Project item)
        {
            var old = Read(item.ProjectId);
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
