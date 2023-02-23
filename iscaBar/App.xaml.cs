﻿using iscaBar.DAO.Servidor;
using iscaBar.Models;
using iscaBar.Services;
using iscaBar.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace iscaBar
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            Order order = null;
            MainPage = new NavigationPage(new ListOrdersView()) ;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
