﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Prog4Project.WPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ManagerEditor(object sender, RoutedEventArgs e)
        {

            ManagerEditorWindow m = new ManagerEditorWindow();
            m.Show();

            //_ManagerTry2xaml try2help = new _ManagerTry2xaml();
            //try2help.Show();

        }

        private void WorkerEditor(object sender, RoutedEventArgs e)
        {
            WorkerWindow w = new WorkerWindow();
            w.Show();
        }

        private void PositionEditor(object sender, RoutedEventArgs e)
        {
            PostionEditorWindow positioneditorial = new PostionEditorWindow();
            positioneditorial.Show();
        }

        private void ProjectEditor(object sender, RoutedEventArgs e)
        {
            ProjectEditorWindow pro = new ProjectEditorWindow();
            pro.Show();
        }
    }
}
