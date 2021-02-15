using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TelerikChartPOC
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new VitalsLandingPage(); // VitalsLandingPage(); // MainPage();
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
