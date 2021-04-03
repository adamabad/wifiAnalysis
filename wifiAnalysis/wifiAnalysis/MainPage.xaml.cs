using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace wifiAnalysis
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private async void StartButton_OnClicked(object sender, EventArgs e) 
        {
            await Navigation.PushAsync(new ScanPage());
        }

        private async void PreviousButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PreviousPage());
        }

        private async void ScanButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScanPageHybrid());
        }

        private async void WebView_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WebViewDemoPage());
        }
    }

}
