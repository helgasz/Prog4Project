using ConsoleTools;
using Prog4Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prog4Project.Client
{
    internal class Program
    {
        static RestService rest;


         static void Update(string v)
        {
            if (v == "Worker")
            {
                Console.Write("Enter Worker's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Worker one = rest.Get<Worker>(id, "worker");
                Console.Write($"New name [old: {one.WorkerName}]: ");
                string name = Console.ReadLine();
                one.WorkerName = name;
                rest.Put(one, "worker");
            }
        }

         static void Delete(string v)
        {
            Console.WriteLine(v + " delete");
            Console.ReadLine();
        }

         static void Create(string v)
        {
            if (v == "Worker")
            {
                Console.Write("Enter Worker Name: ");
                string name = Console.ReadLine();
                rest.Post(new Worker() { WorkerName = name }, "worker");
            }
            
        }

         static void List(string v)
        {
            if (v == "Worker")
            {
                List<Worker> workers = rest.Get<Worker>("worker");
                foreach (var item in workers)
                {
                    Console.WriteLine(item.WorkerId + ": " + item.WorkerName);
                }
            }
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:20741/", "project");

            var workerSubmenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Worker"))
                .Add("Create", () => Create("Worker"))
                .Add("Delete", () => Delete("Worker"))
                .Add("Update", () => Update("Worker"))
                .Add("Exit", ConsoleMenu.Close);

            var positionSubmenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Position"))
                .Add("Create", () => Create("Position"))
                .Add("Delete", () => Delete("Position"))
                .Add("Update", () => Update("Position"))
                .Add("Exit", ConsoleMenu.Close);

            var managerSubmenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Manager"))
                .Add("Create", () => Create("Manager"))
                .Add("Delete", () => Delete("Manager"))
                .Add("Update", () => Update("Manager"))
                .Add("Exit", ConsoleMenu.Close);

            var projectSubmenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Project"))
                .Add("Create", () => Create("Project"))
                .Add("Delete", () => Delete("Project"))
                .Add("Update", () => Update("Project"))
                .Add("Exit", ConsoleMenu.Close);


            var menu = new ConsoleMenu(args, level: 0)
                .Add("Projects", () => projectSubmenu.Show())
                .Add("Workers", () => workerSubmenu.Show())
                .Add("Positions", () => positionSubmenu.Show())
                .Add("Managers", () => managerSubmenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();


        }
    }
}
