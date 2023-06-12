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
            set {
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

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public ProjectWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Projects = new RestCollection<Project>("http://localhost:20741/", "project");
                CreateProjectCommand = new RelayCommand(() =>
                {
                    Projects.Add(new Project()
                    {
                        ProjectName = SelectedProject.ProjectName
                    });
                });
                UpdateProjectCommand = new RelayCommand(() =>
                {
                    Projects.Update(SelectedProject);
                    //try
                    //{
                    //    Projects.Update(SelectedProject);
                    //}
                    //catch (ArgumentException ex)
                    //{
                    //    ErrorMessage = ex.Message;
                    //}
                });
                DeleteProjectCommand = new RelayCommand(() =>
                {
                    Projects.Delete(SelectedProject.ProjectId);
                }, 
                () =>
                {
                    return SelectedProject != null;
                });
                SelectedProject = new Project();
            }
            
        }
    }
}
