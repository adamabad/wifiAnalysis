using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace wifiAnalysis
{
    public class StringToDate : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var item = DateTime.Parse((string)value);
                if (item != null)
                {
                    return item.ToString("f");
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class PreviousPage : ContentPage
    {
        ListView listview;
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
            /*targetList = scans.Select(x => new ScanObjectWithName() {
                    download = x.download,
                    hostname = x.hostname,
                    ip_address = x.ip_address,
                    jitter = x.jitter,
                    latency = x.latency,
                    maxDownload = x.maxDownload,
                    maxUpload = x.maxUpload,
                    testDate = x.testDate,
                    upload = x.upload,
                    testServer = x.testServer,
                    userAgent = x.userAgent,
                    Room_ID = x.Room_ID,
                    isVisible = false,
                })
                .ToList();
            */
            listview.ItemsSource = targetList;
        }
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (listview.SelectedItem != null)
            {
                var detailPage = new DetailPage();
                detailPage.BindingContext = e.SelectedItem as ScanObjectWithName;
                listview.SelectedItem = null;
                await Navigation.PushModalAsync(detailPage);
            }
        }

        public PreviousPage()
        {
            Title = "Previous Scan Results";
            Label header = new Label
            {
                Text = "Previous Scans",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };
            listview = new ListView
            {
                HasUnevenRows = true,
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
                                    VerticalOptions = LayoutOptions.Center,
                                    Spacing = 0,
                                    Children =
                                    {
                                        new StackLayout
                                        {
                                            Orientation = StackOrientation.Horizontal,
                                            Children =
                                            {
                                                roomLabel,
                                                timeLabel
                                            }
                                        },
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

            listview.ItemSelected += OnItemSelected;

            this.Content = new StackLayout
            {
                Children =
                {
                    header,
                    listview
                }
            };
        }
    }
}
