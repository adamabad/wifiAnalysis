using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace wifiAnalysis
{
    public class ScanningPage : ContentPage
    {
        public ScanningPage()
        {
            Title = "Scan Settings";
            int roomNo = 1;
            Label header = new Label
            {
                Text = "WebView",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                //VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center
            };

            WebView webView = new WebView
            {
                Source = new UrlWebViewSource
                {
                    Url = "http://klaipsc.mathcs.wilkes.edu/speedtest",
                },
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            Button viewResultsButton = new Button
            {
                Text = "View Results",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center
            };

            async void onViewButtonClicked(object sender, EventArgs args)
            {
                string result = await webView.EvaluateJavaScriptAsync("parseResults2()");
                ScanObject scanResult = new ScanObject
                {
                    Room_ID = roomNo
                };
                if (result != null)
                {
                    Console.WriteLine(result);
                    jsonScan scanDeserialized = JsonConvert.DeserializeObject<jsonScan>(result);
                    scanResult.download = scanDeserialized.download;
                    scanResult.hostname = scanDeserialized.hostname;
                    scanResult.ip_address = scanDeserialized.ip_address;
                    scanResult.jitter = scanDeserialized.jitter;
                    scanResult.latency = scanDeserialized.latency;
                    scanResult.maxDownload = scanDeserialized.maxDownload;
                    scanResult.maxUpload = scanDeserialized.maxUpload;
                    scanResult.testDate = scanDeserialized.testDate;
                    scanResult.testServer = scanDeserialized.testServer;
                    scanResult.upload = scanDeserialized.upload;
                    scanResult.userAgent = scanDeserialized.userAgent;
                    //await App.ScanDatabase.SaveScanAsync(scanResult);
                    //viewResultsButton.Text = "Database Saved";
                    await Navigation.PushAsync(new ScanResults(scanResult));
                }
                else
                {
                    Console.WriteLine("object is null");
                }
            }

            viewResultsButton.Clicked += onViewButtonClicked;

            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {
                    header,
                    webView,
                    viewResultsButton
                }
            };
        }
    }
}