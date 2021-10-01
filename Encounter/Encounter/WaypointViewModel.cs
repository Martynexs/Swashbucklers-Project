﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Encounter
{
    public class WaypointViewModel : IComparable<WaypointViewModel>
    {
        private Waypoint _waypoint;
        private VisualWaypoint _visualWaypoint;

        public WaypointViewModel()
        {
            _waypoint = new Waypoint();
            _visualWaypoint = new VisualWaypoint();
        }

        public int Index
        {
            get => _waypoint.Index;
            set { _waypoint.Index = value; _visualWaypoint.Index = value; }
        }

        public string Name
        {
            get => _waypoint.Name;
            set { _waypoint.Name = value; _visualWaypoint.Name = value; }
        }

        public (double, double) Coordinates
        {
            get => _waypoint.Coordinates;
            set { _waypoint.Coordinates = value; _visualWaypoint.Coordinates = value; }
        }

        //public string Type { get; set; }

        public decimal Price
        {
            get => _waypoint.Price;
            set => _waypoint.Price = value;
        }

        public DateTime OpeningHours
        {
            get => _waypoint.OpeningHours;
            set => _waypoint.OpeningHours = value;
        }

        public DateTime ClosingTime
        {
            get => _waypoint.ClosingTime;
            set => _waypoint.ClosingTime = value;
        }

        public string Description
        {
            get => _waypoint.Description;
            set => _waypoint.Description = value;
        }

        public StackPanel GetWaypointPanel()
        {
            return _visualWaypoint.GetVisualWaypointPanel();
        }

        public Waypoint GetWaypoint()
        {
            return _waypoint;
        }

        public VisualWaypoint GetVisualWaypoint()
        {
            return _visualWaypoint;
        }

        public int CompareTo(WaypointViewModel other)
        {
            if (other == null) return 1;
            if (other._waypoint.Index > this._waypoint.Index) return -1;
            if (other._waypoint.Index < this._waypoint.Index) return 1;
            return 0;
        }
    }
}
