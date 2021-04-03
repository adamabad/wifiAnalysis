using Plugin.Connectivity;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace wifiAnalysis
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
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
        public bool Internetconnected()
        {
            return CrossConnectivity.Current.IsConnected;
        }

        public bool IsWifiConnected()
        {
            var profiles = Connectivity.ConnectionProfiles;
            if (profiles.Contains(ConnectionProfile.WiFi))
                return true;
            else
                return false;
        }

        public class ConnectivityTest
        {
            public ConnectivityTest()
            {
                Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;

            }

            void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
            {
                var access = e.NetworkAccess;
                var profiles = e.ConnectionProfiles;
            }
        }
    }
}
