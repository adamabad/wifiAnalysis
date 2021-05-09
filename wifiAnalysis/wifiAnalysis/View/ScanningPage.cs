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
        List<RoomObject> rooms;
        Picker picker;
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            rooms = await App.ScanDatabase.GetRooms();
            rooms.Add(new RoomObject
            {
                Room_ID = 0,
                Room_Name = "Not Specified"
            });
            picker.Items.Clear();
            foreach (RoomObject room in rooms)
            {
                picker.Items.Add(room.Room_Name);
            }
        }
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
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.FromHex("1e90ff")
            };

            Button viewResultsButton = new Button
            {
                Text = "View Results",
                VerticalOptions = LayoutOptions.CenterAndExpand,
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

            Button startScanButton = new Button
            { 
                Text = "Start Scan",
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            picker = new Picker
            {
                SelectedIndex = 0,
                TextColor = Color.WhiteSmoke,
                HorizontalTextAlignment = TextAlignment.Center
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
                        scanInprogress = false;
                        MessagingCenter.Subscribe<ScanResults>(this, "ping", (theSender) =>
                        {
                            refresh();
                        });
                        startScanButton.Text = "Start Scan";
                        await Navigation.PushModalAsync(new ScanResults(scanResult));
                    }
                }
                else
                {
                    startScanButton.Text = "See Results";
                    scanInprogress = true;
                    await webView.EvaluateJavaScriptAsync("callStart(" + sliderValue + ")");
                }
            }

            void refresh()
            {
                webView.Reload();
                scanInprogress = false;
                accuracySlider.IsEnabled = true;
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
                    picker,
                    sliderLabel,
                    accuracySlider,
                    startScanButton
                }
            };
        }
    }
}