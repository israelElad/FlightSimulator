﻿using FlightSimulator.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    class FlightModel : BaseNotify
    {
        private double lon;
        public double Lon
        {
            get
            {
                return lon;   
            }
            set
            {
                lon = value;
                NotifyPropertyChanged("Lon");
            }
        }

        private double lat;
        public double Lat
        {
            get
            {
                return lat;
            }
            set
            {
                lat = value;
                NotifyPropertyChanged("Lat"); 
            }
        }

        public void ReadLonAndLat()
        {
            new Thread(delegate ()
            {
                while (true)
                {
                    if (Server.Instance.Data != null)
                    {
                        Lon = Convert.ToDouble(Server.Instance.Data[0]);
                        Lat = Convert.ToDouble(Server.Instance.Data[1]);
                        Thread.Sleep(500);
                    }
                }
            }).Start();
        }
    }
}
