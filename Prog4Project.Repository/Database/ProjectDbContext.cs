using System;
using Microsoft.EntityFrameworkCore;
using Prog4Project.Models;

namespace Prog4Project.Repository
{
    public class ProjectDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<ProjectManager> Managers { get; set; }

        public ProjectDbContext()
        {
            this.Database.EnsureCreated();
        }

        
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder
                    .UseLazyLoadingProxies()
                    .UseInMemoryDatabase("project");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Project>(pro => pro
                .HasOne(pro => pro.Manager)
                .WithMany(ProjectManager => ProjectManager.Projects)
                .HasForeignKey(pro => pro.ManagerId)
                .OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<Worker>()
                .HasMany(x => x.Projects)
                .WithMany(x => x.Workers)
                .UsingEntity<Position>(
                    x => x.HasOne(x => x.Project)
                        .WithMany().HasForeignKey(x => x.ProjectId).OnDelete(DeleteBehavior.Cascade),
                    x => x.HasOne(x => x.Worker)
                        .WithMany().HasForeignKey(x => x.WorkerId).OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<Position>()
                .HasOne(r => r.Worker)
                .WithMany(Worker => Worker.Positions)
                .HasForeignKey(r => r.WorkerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Position>()
                .HasOne(r => r.Project)
                .WithMany(Project => Project.Positions)
                .HasForeignKey(r => r.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Project>().HasData(new Project[]
            {
                new Project("1#Beruhazasi tervezett#100,5#1#2022*03*12#4,9"),
                new Project("2#Arculat tervezes#260,5#2#2021*04*24#7,6"),
                new Project("3#Megujulasi alapok#50,5#3#2020*11*05#3,2"),
                new Project("4#Megujulasi tervezet#175,5#4#2021*02*07#5,9"),
                new Project("5#Logo tervezes#275,4#5#2021*01*12#4,5"),
                new Project("6#Plakat keszites#450#2#2022*06*04#8"),
                new Project("7#Hirdeto anyag tervezese es elkeszitese#1500#5#2023*01*03#9"),
                new Project("8#Beruhazasi kivitelezes#505#1#2023*05*09#9,1"),
                new Project("9#Megujulasi kivitelezes#845,7#4#2022*10*03#8,7"),
                new Project("10#Megujulasi utomunkalatok#200,3#3#2023*03*03#3"),
            });

            modelBuilder.Entity<ProjectManager>().HasData(new ProjectManager[]
            {
                new ProjectManager("1#Bob Simsly"),
                new ProjectManager("2#Jessica Parker"),
                new ProjectManager("3#Miriam Lovelace"),
                new ProjectManager("4#Kim Hyun-Soo"),
                new ProjectManager("5#Lee Min Ho"),
            });

            modelBuilder.Entity<Worker>().HasData(new Worker[]
            {
                new Worker("1#Lee Know"),
                new Worker("2#Choi San"),
                new Worker("3#Leonard Potter"),
                new Worker("4#Neil Korain"),
                new Worker("5#Willow Prolle"),
                new Worker("6#Cassandra Goth"),

            });

            modelBuilder.Entity<Position>().HasData(new Position[]
            {
                new Position("1#1#1#2#Grafikus tervezo"),
                new Position("2#2#1#3#Grafikus tervezo"),
                new Position("3#1#2#3#Elemi tanacsado"),
                new Position("4#2#2#5#Vazlat keszito"),
                new Position("5#3#3#4#Elemi kivitelezo"),
                new Position("6#4#3#3#Tervosszesito"),
                new Position("7#3#4#1#Vezeto tervezo"),
                new Position("8#4#4#4#Altalanos tanacsado"),
                new Position("9#5#5#2#Vezeto grafikus"),
                new Position("10#6#5#5#Grafikus asszisztens"),
                new Position("11#5#6#8#Asszisztens grafikus"),
                new Position("12#6#6#2#Tervezo es kivitelezo grafikus"),
                new Position("13#7#1#4#Grafikus asszisztens"),
                new Position("14#7#2#10#Beszerzesi tanacsado"),
                new Position("15#8#3#5#Uzleti tanacsado"),
                new Position("16#8#4#3#Vezeto tervezo"),
                new Position("17#9#4#10#Tanacsado"),
                new Position("18#9#5#10#Tanacsado"),
                new Position("19#10#6#5#Feladatellenorzo"),
                new Position("20#10#1#2#Folyamatvezerlo"),

            });
        }

    }
}
