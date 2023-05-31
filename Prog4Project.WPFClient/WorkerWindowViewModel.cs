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
    public class WorkerWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }
        public RestCollection<Worker> workers { get; set; }

        private Worker selectedWorker;

        public Worker SelectedWorker
        {
            get { return selectedWorker; }
            set
            {
                if (value != null)
                {
                    selectedWorker = new Worker()
                    {
                        WorkerName = value.WorkerName,
                        WorkerId = value.WorkerId
                    };
                }
                OnPropertyChanged();
                (DeleteWorkerCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }


        public ICommand CreateWorkerCommand { get; set; }
        public ICommand DeleteWorkerCommand { get; set; }
        public ICommand UpdateWorkerCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public WorkerWindowViewModel()
        {


            if (!IsInDesignMode)
            {
                workers = new RestCollection<Worker>("http://localhost:20741/", "worker");
                CreateWorkerCommand = new RelayCommand(() =>
                {
                    workers.Add(new Worker()
                    {
                        WorkerName = SelectedWorker.WorkerName
                    });
                });
                DeleteWorkerCommand = new RelayCommand(() =>
                {
                    workers.Delete(SelectedWorker.WorkerId);

                },
                () =>
                {
                    return SelectedWorker != null;
                });

                UpdateWorkerCommand = new RelayCommand(() =>
                {
                    try
                    {
                        workers.Update(SelectedWorker);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });
                SelectedWorker = new Worker();
            }

        }
    }
}
