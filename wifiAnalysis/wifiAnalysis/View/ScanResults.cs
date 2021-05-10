using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using Xamarin.Forms;
using wifiAnalysis.View;

namespace wifiAnalysis
{
    public partial class ScanResults : ContentPage
    {
        ScanObject Scan;
        Button SaveButton;
        Button TipsButton;
        Button MenuButton;
        Button ScanButton;
        ListView listView;
        Label downloadLabel;
        Label uploadLabel;
        Label jitterLabel;
        Label pingLabel;
        Label maxDLabel;
        Label maxULabel;
        List<RoomObject> rooms;
        List<ScanObjectWithName> targetList; 

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            rooms = await App.ScanDatabase.GetRooms();
            List<ScanObject> scans = await App.ScanDatabase.GetFiveScans();
            targetList = (from s in scans
                          join r in rooms on s.Room_ID equals r.Room_ID
                          select new ScanObjectWithName
                          {
                              Room_Name = r.Room_Name,
                              download = s.download,
                              hostname = s.hostname,
                              ip_address = s.ip_address,
                              jitter = s.jitter,
                              latency = s.latency,
                              maxDownload = s.maxDownload,
                              maxUpload = s.maxUpload,
                              testDate = s.testDate,
                              upload = s.upload,
                              testServer = s.testServer,
                              userAgent = s.userAgent,
                              Room_ID = s.Room_ID,
                              isVisible = false,
                          }).ToList();
            targetList.Reverse();
            listView.ItemsSource = targetList;
        }
        public ScanResults(ScanObject scanResults, string Room_Name)
        {
            Title = "Scan Results";
            Scan = scanResults;
            SetBinding(ContentPage.TitleProperty, new Binding("Room_Name"));
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += onTapped;

            var header = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                FontAttributes = FontAttributes.Bold,
                Text = Room_Name
            };

            var dateLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Text = DateTime.Parse(scanResults.testDate).ToString("f")
            };

            var line = new BoxView
            {
                HeightRequest = 1,
                Color = Color.DarkGray,
            };

            downloadLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                Text = string.Format("{0} Mbps", scanResults.download.ToString()),
                TextColor = (scanResults.download>25)? Color.Green : (scanResults.download>12)? Color.Orange : Color.Red
            };
            downloadLabel.GestureRecognizers.Add(tapGestureRecognizer);

            uploadLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                Text = string.Format("{0} Mbps", scanResults.upload.ToString()),
                TextColor = (scanResults.upload>25) ? Color.Green : (scanResults.upload>12)? Color.Orange : Color.Red
            };
            uploadLabel.GestureRecognizers.Add(tapGestureRecognizer);

            var hostnameLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                Text = scanResults.hostname
            };

            var ipLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                Text = scanResults.ip_address
            };

            jitterLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                Text = string.Format("{0} ms", scanResults.jitter.ToString()),
                TextColor = (scanResults.jitter < 50) ? Color.Green : (scanResults.jitter < 100) ? Color.Orange : Color.Red
            };
            jitterLabel.GestureRecognizers.Add(tapGestureRecognizer);

            pingLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                Text = string.Format("{0} ms", scanResults.latency.ToString()),
                TextColor = (scanResults.latency < 150) ? Color.Green : (scanResults.latency < 400) ? Color.Orange : Color.Red
            };
            pingLabel.GestureRecognizers.Add(tapGestureRecognizer);

            maxDLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                Text = string.Format("{0} Mbps", scanResults.maxDownload.ToString()),
                TextColor = (scanResults.download > 25) ? Color.Green : (scanResults.download > 12) ? Color.Orange : Color.Red
            };
            maxDLabel.GestureRecognizers.Add(tapGestureRecognizer);

            maxULabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                Text = string.Format("{0} Mbps", scanResults.maxUpload.ToString()),
                TextColor = (scanResults.maxUpload > 25) ? Color.Green : (scanResults.maxUpload > 12) ? Color.Orange : Color.Red
            };
            maxULabel.GestureRecognizers.Add(tapGestureRecognizer);

            var previousLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center,
                Padding = new Thickness(0, 40, 0, 0),
                Text = "Recent Scans"
            };

            SaveButton = new Button { Text = "Save Scan Results" };
            TipsButton = new Button { Text = "View Connectivity Tips" };
            MenuButton = new Button { Text = "Home", HorizontalOptions = LayoutOptions.FillAndExpand};
            ScanButton = new Button { Text = "Scan Again", HorizontalOptions = LayoutOptions.FillAndExpand };

            SaveButton.Clicked += OnSaveButtonClicked;
            TipsButton.Clicked += OnTipsButtonClicked;
            MenuButton.Clicked += MenuButton_Clicked;
            ScanButton.Clicked += ScanButton_Clicked;

            listView = new ListView
            {
                HasUnevenRows = true,
                SeparatorVisibility = SeparatorVisibility.Default,
                SelectionMode = ListViewSelectionMode.None,
                ItemTemplate = new DataTemplate(() =>
                {

                    Label roomLabel = new Label();
                    roomLabel.SetBinding(Label.TextProperty, "Room_Name");
                    roomLabel.FontAttributes = FontAttributes.Bold;

                    Label timeLabel = new Label();
                    timeLabel.SetBinding(Label.TextProperty,
                        new Binding("testDate", BindingMode.OneWay, new StringToDate(), null, "{0}"));

                    Label download = new Label();
                    download.SetBinding(Label.TextProperty,
                        new Binding("download", BindingMode.OneWay, null, null, "Download: {0} Mbps"));

                    Label upload = new Label();
                    upload.SetBinding(Label.TextProperty,
                        new Binding("upload", BindingMode.OneWay, null, null, "Upload: {0} Mbps"));

                    Label ping = new Label();
                    ping.SetBinding(Label.TextProperty,
                        new Binding("latency", BindingMode.OneWay, null, null, "Ping: {0:d} ms"));

                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(0, 5),
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            //VerticalOptions = LayoutOptions.Center,
                            Children =
                            {
                                new StackLayout
                                {
                                    HorizontalOptions = LayoutOptions.FillAndExpand,
                                    Children =
                                    {
                                        roomLabel,
                                        timeLabel
                                    }
                                },
                                new StackLayout
                                {
                                    HorizontalOptions = LayoutOptions.FillAndExpand,
                                    Children =
                                    {
                                        download,
                                        upload,
                                        ping
                                    }
                                }
                            }
                        }

                    };
                })
            };
            Content = new StackLayout
            {
                Padding = new Thickness(5),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                    header,
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.Center,
                        Children =
                        {
                            dateLabel
                        }
                    },
                    line,
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            new Label{ Text = "Download:", FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)), HorizontalOptions = LayoutOptions.FillAndExpand },
                            downloadLabel,
                        }
                    },
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            new Label{ Text = "Upload:", FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)), HorizontalOptions = LayoutOptions.FillAndExpand },
                            uploadLabel,
                        }
                    },
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            new Label{ Text = "Latency:", FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)), HorizontalOptions = LayoutOptions.FillAndExpand },
                            pingLabel,
                        }
                    },
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            new Label{ Text = "Jitter:", FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)), HorizontalOptions = LayoutOptions.FillAndExpand },
                            jitterLabel,
                        }
                    },
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            new Label{ Text = "Max Download:", FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)), HorizontalOptions = LayoutOptions.FillAndExpand },
                            maxDLabel,
                        }
                    },
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            new Label{ Text = "Max Upload:", FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)), HorizontalOptions = LayoutOptions.FillAndExpand },
                            maxULabel,
                        }
                    },
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            new Label{ Text = "IP Address:", FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)), HorizontalOptions = LayoutOptions.FillAndExpand },
                            ipLabel,
                        }
                    },
                    new Label{ Text = "Host:", FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)), HorizontalOptions = LayoutOptions.FillAndExpand },
                    hostnameLabel,
                    TipsButton,
                    SaveButton,
                    previousLabel,
                    new BoxView { HeightRequest = 1, Color = Color.DarkGray },
                    listView,
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Children =
                        {
                            MenuButton,
                            ScanButton,
                        }
                    },
                }
            };
        }
        async void onTapped(object sender, EventArgs e)
        {
            if (sender == downloadLabel || sender == maxDLabel)
            { 
                await DisplayAlert("Download Speed", "Color Cutoffs and Meanings:\nRed(Below Satisfactory) - Less than 12Mbps\nYellow(Satisfactory) - Between 12 and 25Mbps\nGreen(Exceptional) - More than 25Mbps", "Close");
            };
            if (sender == uploadLabel || sender == maxULabel)
            {
                await DisplayAlert("Upload Speed", "Color Cutoffs and Meanings:\nRed(Below Satisfactory) - Less than 12Mbps\nYellow(Satisfactory) - Between 12 and 25Mbps\nGreen(Exceptional) - More than 25Mbps", "Close");
            };
            if (sender == jitterLabel)
            {
                await DisplayAlert("Jitter", "Color Cutoffs and Meanings:\nRed(Below Satisfactory) - More than 100Mbps\nYellow(Satisfactory) - Between 100 and 50Mbps\nGreen(Exceptional) - Less than 50Mbps", "Close");
            };
            if (sender == pingLabel)
            {
                await DisplayAlert("Latency", "Color Cutoffs and Meanings:\nRed(Below Satisfactory) - More than 400Mbps\nYellow(Satisfactory) - Between 400 and 100Mbps\nGreen(Exceptional) - Less than 100Mbps", "Close");
            };
        }

        async void ScanButton_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send<ScanResults>(this, "ping");
            await Navigation.PopModalAsync();
        }

        private void MenuButton_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new MainPage());
        }

        async void OnTipsButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new TipsPage());
        }    

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                await App.ScanDatabase.SaveScanAsync(Scan);
                SaveButton.Text = "Saved.";
            }
            catch (Exception)
            {
                SaveButton.Text = "Error.";
            }
           
            SaveButton.IsEnabled = false;
        }
    }
}
