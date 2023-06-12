using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Prog4Project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Prog4Project.WPFClient
{

    public class ProjectWindowViewModel : ObservableRecipient
    {

        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }
        public RestCollection<Project> Projects { get; set; }

        private Project selectedProject;

        public Project SelectedProject
        {
            get { return selectedProject; }
            set
            {
                if (value != null)
                {
                    selectedProject = new Project()
                    {
                        ProjectName = value.ProjectName,
                        ProjectId = value.ProjectId
                    };
                    OnPropertyChanged();
                }

                (DeleteProjectCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        public ICommand CreateProjectCommand { get; set; }
        public ICommand DeleteProjectCommand { get; set; }
        public ICommand UpdateProjectCommand { get; set; }

        public ICommand AvarageDiffMGT { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        private string managerIdGiven;

        public string ManagerIdGiven
        {
            get { return managerIdGiven; }
            set { SetProperty(ref managerIdGiven, value); }
        }

        private int managerProjectCount;

        public int ManagerProjectCount
        {
            get { return managerProjectCount; }
            set { SetProperty(ref managerProjectCount, value); }
        }

        private double managerDifficultyAverage;

        public double ManagerDifficultyAverage
        {
            get { return managerDifficultyAverage; }
            set { SetProperty(ref managerDifficultyAverage, value); }
        }

        public int GetProjectCountByManagerID(int managerID)
        {
            var projectsWithManager = Projects.Where(p => p.ManagerId == managerID);

            ManagerProjectCount = projectsWithManager.Count();

            return ManagerProjectCount;
        }
        public double GetAverageDifficultyByManagerID(int managerID)
        {
            var projectsWithManager = Projects.Where(p => p.ManagerId == managerID && p.Difficulity != default(double));

            if (projectsWithManager.Any())
            {
                return projectsWithManager.Average(p => p.Difficulity);
            }
            else
            {
                return 0.0; 
            }
        }
        
        public ProjectWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Projects = new RestCollection<Project>("http://localhost:20741/", "project");
                CreateProjectCommand = new RelayCommand(() =>
                {
                    if (SelectedProject != null)
                    {
                        Projects.Add(new Project()
                        {
                            ProjectName = SelectedProject.ProjectName
                        });
                    }
                });
                UpdateProjectCommand = new RelayCommand(() =>
                {

                    try
                    {
                        Projects.Update(SelectedProject);

                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }
                });
                DeleteProjectCommand = new RelayCommand(() =>
                {
                    Projects.Delete(SelectedProject.ProjectId);
                },
                () =>
                {
                    return SelectedProject != null;
                });
                
                AvarageDiffMGT = new RelayCommand(() =>
                {
                    int managerId;
                    if (int.TryParse(ManagerIdGiven, out managerId))
                    {
                        ManagerDifficultyAverage = GetAverageDifficultyByManagerID(managerId);
                        ManagerProjectCount = GetProjectCountByManagerID(managerId);
                    }
                    else
                    {

                        ManagerDifficultyAverage = 0.0;
                        ManagerProjectCount = 0;
                    }
                });
                SelectedProject = new Project();
            }

        }

    }
}

