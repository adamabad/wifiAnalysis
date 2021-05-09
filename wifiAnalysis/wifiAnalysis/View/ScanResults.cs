using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using Xamarin.Forms;

namespace wifiAnalysis
{
    public partial class ScanResults : ContentPage
    {
        ScanObject Scan;
        Button SaveButton;
        Button MenuButton;
        Button ScanButton;
        ListView listView;
        List<ScanObjectWithName> targetList;
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            List<RoomObject> rooms = await App.ScanDatabase.GetRooms();
            List<ScanObject> scans = await App.ScanDatabase.GetScanResults();
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

        public ScanResults(ScanObject scanResults)
        {
            Title = "Scan Results";
            Scan = scanResults;
            SetBinding(ContentPage.TitleProperty, new Binding("Room_Name"));
            var header = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                Text = scanResults.Room_ID.ToString() 
            };

            var dateLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Text = DateTime.Parse(scanResults.testDate).ToString("f")
            };

            var downloadLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                Text = string.Format("{0} Mbps", scanResults.download.ToString())
            };

            var uploadLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                Text = string.Format("{0} Mbps", scanResults.upload.ToString())
            };

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

            var jitterLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                Text = string.Format("{0} ms", scanResults.jitter.ToString())
            };

            var pingLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                Text = string.Format("{0} ms", scanResults.latency.ToString())
            };

            var maxDLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                Text = string.Format("{0} Mbps", scanResults.maxDownload.ToString())
            };

            var maxULabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                Text = string.Format("{0} Mbps", scanResults.maxUpload.ToString())
            };

            SaveButton = new Button { Text = "Save Results" };
            MenuButton = new Button { Text = "Home" };
            ScanButton = new Button { Text = "Scan Again" };

            SaveButton.Clicked += OnSaveButtonClicked;
            MenuButton.Clicked += MenuButton_Clicked;
            ScanButton.Clicked += ScanButton_Clicked;

            listView = new ListView
            {
                HasUnevenRows = true,
                SelectionMode = ListViewSelectionMode.None,
                ItemTemplate = new DataTemplate(() =>
                {
                    Label roomLabel = new Label();
                    roomLabel.SetBinding(Label.TextProperty, "Room_Name");
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
                            VerticalOptions = LayoutOptions.Center,
                            Children =
                            {
                                new StackLayout
                                {
                                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                                    Children =
                                    {
                                        roomLabel,
                                        timeLabel
                                    }
                                },
                                new StackLayout
                                {
                                    HorizontalOptions = LayoutOptions.CenterAndExpand,
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
                    listView,
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Children =
                        {
                            MenuButton,
                            ScanButton,
                            SaveButton
                        }
                    },
                }
            };
        }

        async void ScanButton_Clicked(object sender, EventArgs e)
        { 
            Navigation.InsertPageBefore(new ScanningPage(), Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count]);
            await Navigation.PopModalAsync();
        }

        private void MenuButton_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new MainPage());
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            await App.ScanDatabase.SaveScanAsync(Scan);
            SaveButton.Text = "Saved.";
            SaveButton.IsEnabled = false;
        }
    }
}
