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
using System.Net.Sockets;
using FlightSimulator.ViewModels;
using FlightSimulator.Model;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Microsoft.Research.DynamicDataDisplay;
using System.ComponentModel;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for Flight.xaml
    /// </summary>
    public partial class FlightView : UserControl
    {
        ObservableDataSource<Point> planeLocations = null;
        public FlightViewModel viewModel;

        public FlightView()
        {
            InitializeComponent();
            this.viewModel = new FlightViewModel();
            //if a property changes in viewModel- activate Vm_PropertyChanged.
            viewModel.PropertyChanged += Vm_PropertyChanged;
            DataContext = viewModel;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            planeLocations = new ObservableDataSource<Point>();
            // Set identity mapping of point in collection to point on plot
            planeLocations.SetXYMapping(p => p);
            plotter.AddLineGraph(planeLocations, 2, "Route");
        }

        /*
        * If a property with one of the names specified changes-
        * write the new point on the FlightBoard.
        * */
        private void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if ((e.PropertyName.Equals("Lat") || e.PropertyName.Equals("Lon")) && (viewModel.Lat != 0 && viewModel.Lon != 0))
            {
                Point p1 = new Point(viewModel.Lat, viewModel.Lon);
                planeLocations.AppendAsync(Dispatcher, p1);
            }
        }
    }
}
