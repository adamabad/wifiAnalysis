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
            bool scanInprogress = false;
            int sliderValue = 5;

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
                    Url = "http://klaipsc.mathcs.wilkes.edu/speedtest2",
                },
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            Button startScanButton = new Button
            {
                Text = "Start Scan",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };

            Label sliderLabel = new Label
            {
                Text = "Level of Accuracy: " + sliderValue,
                TextColor = Color.FromHex("f5f5f5"),
                HorizontalOptions = LayoutOptions.Center,
            };
            Slider accuracySlider = new Slider
            {
                Maximum = 10,
                Minimum = 1,
                Value = sliderValue,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            async void onStartScanButtonClicked(object sender, EventArgs args)
            {
                if (scanInprogress)
                {
                    string result = await webView.EvaluateJavaScriptAsync("parseResults2()");
                    accuracySlider.IsEnabled = false;
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
                        await App.ScanDatabase.SaveScanAsync(scanResult);
                        //saveResultsButton.Text = "Database Saved";
                        //await Navigation.PushAsync(new ScanProgressPage(scanResult));
                    }
                    else
                    {
                        Console.WriteLine("object is null");
                    }
                }
                else
                {
                    startScanButton.Text = "See Results";
                    scanInprogress = true;
                    await webView.EvaluateJavaScriptAsync("callStart(" + sliderValue + ")");
                }
            }

            void AccuracySlider_ValueChanged(object sender, ValueChangedEventArgs e)
            {
                sliderValue = (int)Math.Round(accuracySlider.Value);
                sliderLabel.Text = "Level of Accuracy: " + sliderValue;
            }

            startScanButton.Clicked += onStartScanButtonClicked;
            accuracySlider.ValueChanged += AccuracySlider_ValueChanged;
            // Build the page.
            this.Content = new StackLayout
            {
                BackgroundColor = Color.FromHex("1e90ff"),
                Children =
                {
                    webView,
                    sliderLabel,
                    accuracySlider,
                    startScanButton
                }
            };
        }
    }
}