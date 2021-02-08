using LojistikProject.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LojistikProject
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage =new NavigationPage(new AnaSayfa());
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
