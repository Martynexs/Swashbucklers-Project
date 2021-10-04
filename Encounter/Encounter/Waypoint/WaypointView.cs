﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Encounter.Commands;
using Encounter.Stores;

namespace Encounter
{
    public class WaypointView
    {
        ICommand SelectWaypoint { get; }

        private int _index;
        public int Index
        {
            get => _index;
            set { _index = value; _numberLabel.Content = Index.ToString() + "."; _button.Tag = value; }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set { _name = value; SetInfoLabel(); }
        }

        private (double, double) _coordinates;
        public (double, double) Coordinates
        {
            get => _coordinates;
            set { _coordinates = value; SetInfoLabel(); }
        }

        private void SetInfoLabel()
        {
            _infoLabel.Content = Name + " (" + Coordinates.Item1.ToString() + ", " + Coordinates.Item2.ToString() + ")";
        }

        private StackPanel _stackPanel;
        private Ellipse _ellipse;
        private Button _button;
        private Label _numberLabel;
        private Label _infoLabel;

        public WaypointView(ICommand buttonCommand)
        {
            SelectWaypoint = buttonCommand;

            _stackPanel = new StackPanel();
            _stackPanel.Orientation = Orientation.Horizontal;
            _stackPanel.Height = 60;
            _stackPanel.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFE6E6E6");

            _numberLabel = new Label
            {
                Width = 50,
                HorizontalContentAlignment = HorizontalAlignment.Right,
                FontSize = 18,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontWeight = FontWeights.Bold
            };
            _stackPanel.Children.Add(_numberLabel);

            _ellipse = new Ellipse
            {
                Height = 50,
                Stroke = Brushes.Black,
                StrokeThickness = 4,
                Width = 52,
                Fill = Brushes.Yellow,
                Cursor = Cursors.Hand,
                StrokeDashCap = PenLineCap.Round
            };

            _button = new Button
            {
                Width = 57,
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent
            };
            _button.Content = _ellipse;
            _button.Command = SelectWaypoint;
            _stackPanel.Children.Add(_button);

            _infoLabel = new Label
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 18
            };
            _stackPanel.Children.Add(_infoLabel);
            DockPanel.SetDock(_stackPanel, Dock.Top);
        }

        public StackPanel GetWaypointViewPanel()
        {
            return _stackPanel;
        }

    }
}