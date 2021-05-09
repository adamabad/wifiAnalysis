using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace wifiAnalysis
{
    public class ScanningPage : ContentPage
    {
        List<RoomObject> rooms;
        Picker picker;
        Entry entry;
        bool rescan = false;
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            rooms = await App.ScanDatabase.GetRooms();
            rooms.Reverse();
            picker.Items.Clear();
            foreach (RoomObject room in rooms)
            {
                picker.Items.Add(room.Room_Name);
            }
            if (!rescan)
            {
                picker.SelectedIndex = rooms.Count - 1;
            } 
        }
        public ScanningPage()
        {
            Title = "Scan Settings";
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
            entry = new Entry
            {
                Placeholder = "Enter Room Name",
                IsVisible = false
            };
            picker = new Picker
            {
                Title = "Select a Room",
                TextColor = Color.WhiteSmoke,
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            Button addRoom = new Button
            {
                Text = "Add Room"
            };

            async void AddRoom_Clicked(object sender, EventArgs e)
            {
                entry.IsVisible = true;
                await Task.Delay(500);
                entry.Focus();
            }

            async void Entry_Completed(object sender, EventArgs e)
            {
                entry.IsVisible = false;
                try
                {
                    RoomObject room = new RoomObject { Room_Name = entry.Text};
                    await App.ScanDatabase.SaveRoomAsync(room);

                    rooms = await App.ScanDatabase.GetRooms();
                    rooms.Add(new RoomObject
                    {
                        Room_ID = 0,
                        Room_Name = "Not Specified"
                    });
                    picker.Items.Clear();
                    foreach (RoomObject myroom in rooms)
                    {
                        picker.Items.Add(myroom.Room_Name);
                    }
                    picker.SelectedIndex = rooms.FindIndex(x => x.Room_Name.Equals(room.Room_Name));
                }
                catch (Exception)
                {
                    await DisplayAlert("Error", "Could not save room.", "OK");
                }
                entry.Text = "";
            }

            async void onStartScanButtonClicked(object sender, EventArgs args)
            {
                if (scanInprogress)
                {
                    string result = await webView.EvaluateJavaScriptAsync("parseResults2()");
                    accuracySlider.IsEnabled = false;
                    ScanObject scanResult = new ScanObject
                    {
                        Room_ID = rooms.Find(x => x.Room_Name.Equals(picker.Items[picker.SelectedIndex])).Room_ID
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
                        //saveResultsButton.Text = "Database Saved";
                        //await Navigation.PushAsync(new ScanProgressPage(scanResult));
                        scanInprogress = false;
                        MessagingCenter.Subscribe<ScanResults>(this, "ping", (theSender) =>
                        {
                            refresh();
                        });
                        string Room_Name = picker.SelectedItem.ToString();
                        startScanButton.Text = "Start Scan";
                        await Navigation.PushModalAsync(new ScanResults(scanResult, Room_Name));
                    }
                }
                else
                {
                    startScanButton.Text = "See Results";
                    scanInprogress = true;
                    picker.IsEnabled = false;
                    accuracySlider.IsEnabled = false;
                    await webView.EvaluateJavaScriptAsync("callStart(" + sliderValue + ")");
                }
            }

            void refresh()
            {
                rescan = true;
                webView.Reload();
                scanInprogress = false;
                accuracySlider.IsEnabled = true;
                picker.IsEnabled = true;
            }

            void AccuracySlider_ValueChanged(object sender, ValueChangedEventArgs e)
            {
                sliderValue = (int)Math.Round(accuracySlider.Value);
                sliderLabel.Text = "Level of Accuracy: " + sliderValue;
            }

            startScanButton.Clicked += onStartScanButtonClicked;
            accuracySlider.ValueChanged += AccuracySlider_ValueChanged;
            addRoom.Clicked += AddRoom_Clicked;
            entry.Completed += Entry_Completed;

            // Build the page.
            this.Content = new StackLayout
            {
                BackgroundColor = Color.FromHex("1e90ff"),
                Children =
                {
                    webView,
                    entry,
                    new StackLayout
                    { 
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Children =
                        { 
                            addRoom,
                            picker
                        }
                    },
                    sliderLabel,
                    accuracySlider,
                    startScanButton
                }
            };
        }
    }
}