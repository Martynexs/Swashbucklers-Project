﻿using Encounter.IO;
using Encounter.Models;
using Encounter.Stores;
using Encounter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Encounter.Commands
{
    public class CreateNewRouteCommand : CommandBase
    {
        private Route _route;
        private NavigationStore _navigationStore;
        public CreateNewRouteCommand(Route route, NavigationStore navigationStore)
        {
            _route = route;
            _navigationStore = navigationStore;
        }

        public override void Execute(object parameter)
        {
            if (_route.Name != null)
            {
                try
                {
                    DatabaseFunctions.CreateRoute(_route);
                    _navigationStore.CurrentViewModel = new RouteViewModel(_navigationStore, _route);
                }
                catch
                {
                    MessageBox.Show("Invalid route name");
                }
            }
        }
    }
}