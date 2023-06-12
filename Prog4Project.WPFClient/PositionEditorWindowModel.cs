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
    public class PositionEditorWindowModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }
        public RestCollection<Position> Positions { get; set; }

        private Position selectedPosition;

        public Position SelectedPosition
        {
            get { return selectedPosition; }
            set
            {
                if (value!=null)
                {
                    selectedPosition = new Position()
                    {
                        PositionName = value.PositionName,
                        PositionId = value.PositionId
                    };
                    OnPropertyChanged();
                }
                
                (DeletePositionCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }


        public ICommand CreatePositionCommand { get; set; }

        public ICommand DeletePositionCommand { get; set; }
        public ICommand UpdatePositionCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public PositionEditorWindowModel()
        {
            
            if (!IsInDesignMode)
            {
                Positions = new RestCollection<Position>("http://localhost:20741/", "position");
                CreatePositionCommand = new RelayCommand(() =>
                {
                    Positions.Add(new Position()
                    {
                        PositionName = SelectedPosition.PositionName
                    });
                });

                UpdatePositionCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Positions.Update(SelectedPosition);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }
                });

                DeletePositionCommand = new RelayCommand(() =>
                {
                    Positions.Delete(SelectedPosition.PositionId);
                },
                () =>
                {
                    return SelectedPosition != null;
                });
                SelectedPosition = new Position();
            }
            
        }
    }
}
