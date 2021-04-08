using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Linq;

namespace wifiAnalysis
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanProgressPage : ContentPage
    {
        
        public ScanProgressPage()
        {
            InitializeComponent();
        }
        public string getLinkSpeed() {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                var profiles = Connectivity.ConnectionProfiles;
                if (profiles.Contains(ConnectionProfile.WiFi))
                {
                    
                }
            }
            return "";
        }
    }
}