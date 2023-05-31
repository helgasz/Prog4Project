using System;
using System.Collections.Generic;
using System.Linq;
using Prog4Project.Models;
using Prog4Project.Repository;

namespace Prog4Project.Logic
{
    public class ProjectLogic : IProjectLogic
    {
        IRepository<Project> repo;

        public ProjectLogic(IRepository<Project> repo)
        {
            this.repo = repo;
        }

        public void Create(Project item)
        {
            if (item.ProjectName.Length < 3)
            {
                throw new ArgumentException("Project name too short");
            }
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Project Read(int id)
        {
            var project = this.repo.Read(id);
            if (project == null)
            {
                throw new ArgumentException("Project not exist");
            }
            return project;
        }

        public IQueryable<Project> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Project item)
        {
            this.repo.Update(item);
        }

        public double? GetAvarageDifficulityPerYear(int year)
        {
            return this.repo
                .ReadAll()
                .Where(t => t.Finished.Year == year)
                .Average(t => t.Difficulity);
        }

        public IEnumerable<YearInfo> YearStatistics()
        {
            return from x in this.repo.ReadAll()
                   group x by x.Finished.Year into g
                   select new YearInfo()
                   {
                       Year = g.Key,
                       AvgRating = g.Average(t => t.Difficulity),
                       ProjectNum = g.Count()
                   };

        }

        public class YearInfo
        {
            public int Year { get; set; }
            public double? AvgRating { get; set; }

            public int ProjectNum { get; set; }
            public int PosNum { get; set; }

        }


    }
}
