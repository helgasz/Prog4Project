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
    public class ManagerWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }
        public RestCollection<ProjectManager> managers { get; set; }

        private ProjectManager selectedManager;

        public ProjectManager SelectedManager
        {
            get { return selectedManager; }
            set
            {
                if (value != null)
                {
                    selectedManager = new ProjectManager()
                    {
                        ManagerName = value.ManagerName,
                        ManagerId = value.ManagerId
                    };
                }
                OnPropertyChanged();
                (DeleteManagerCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }


        public ICommand CreateManagerCommand { get; set; }
        public ICommand DeleteManagerCommand { get; set; }
        public ICommand UpdateManagerCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public ManagerWindowViewModel()
        {


            if (!IsInDesignMode)
            {
                managers = new RestCollection<ProjectManager>("http://localhost:20741/", "Manager");
                CreateManagerCommand = new RelayCommand(() =>
                {
                    managers.Add(new ProjectManager()
                    {
                        ManagerName = SelectedManager.ManagerName
                    });
                });
                DeleteManagerCommand = new RelayCommand(() =>
                {
                    managers.Delete(SelectedManager.ManagerId);

                },
                () =>
                {
                    return SelectedManager != null;
                });

                UpdateManagerCommand = new RelayCommand(() =>
                {
                    try
                    {
                        managers.Update(SelectedManager);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });
                SelectedManager = new ProjectManager();
            }

        }
    }
}
