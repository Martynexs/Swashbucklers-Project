﻿using PSI.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PSI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyRouteAboutPage : ContentPage
    {
        public MyRouteAboutPage()
        {
            InitializeComponent();
            BindingContext = new RouteDetailViewModel();
        }
    }
}