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
            var existingPages = Navigation.NavigationStack.ToList();
            foreach (var page in existingPages)
            {
                Navigation.RemovePage(page);
            }
            InitializeComponent();
        }

        private async void PreviousButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PreviousPage());
        }

        private async void StartScan_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScanningPage());
        }
    }

}
