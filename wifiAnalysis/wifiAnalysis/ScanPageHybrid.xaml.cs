using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace wifiAnalysis
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanPageHybrid : ContentPage
    {
        public ScanPageHybrid()
        {
            InitializeComponent();

            hybridWebView.RegisterAction(data => DisplayAlert("Alert", "Hello " + data, "OK"));

            //var hybridWebView = new HybridWebView
            //{
            //    Uri = "http://klaipsc.mathcs.wilkes.edu/speedtest"
            //};

            ////hybridWebView.RegisterAction(data => DisplayAlert("Alert", "Hello " + data, "OK"));

            //Padding = new Thickness(0, 40, 0, 0);
            //Content = hybridWebView;
        }
    }
}