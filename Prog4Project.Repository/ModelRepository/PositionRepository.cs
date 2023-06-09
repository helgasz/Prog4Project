﻿using Prog4Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog4Project.Repository
{
    public class PositionRepository : Repository<Position>, IRepository<Position>
    {
        public PositionRepository(ProjectDbContext ctx) : base(ctx)
        {
        }

        public override Position Read(int id)
        {
            return ctx.Positions.FirstOrDefault(t => t.PositionId == id);
        }

        public override void Update(Position item)
        {
            var old = Read(item.PositionId);
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
