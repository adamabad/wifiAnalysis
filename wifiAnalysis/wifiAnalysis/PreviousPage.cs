using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace wifiAnalysis
{
    class PreviousPage : ContentPage
    {
        ListView listview;
        List<ScanObjectWithName> targetList;
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            List<RoomObject> rooms = await App.ScanDatabase.GetRooms();
            List<ScanObject> scans = await App.ScanDatabase.GetScanResults();
            targetList = scans.Select(x => new ScanObjectWithName() {
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
                    //Room_Name = rooms.Find(i => i.Room_ID.Equals(x.Room_ID)).Room_Name,
                    Room_ID = x.Room_ID,
                    isVisible = false,
                })
                .ToList();

            listview.ItemsSource = targetList;
        }
        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var scan = e.Item as ScanObjectWithName;
            scan.isVisible = true;
            int index = targetList.IndexOf(scan);
            targetList.Remove(scan);
            targetList.Insert(index, scan);
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
                SelectionMode = ListViewSelectionMode.None,
                ItemTemplate = new DataTemplate(() =>
                {
                    Label roomLabel = new Label();
                    roomLabel.SetBinding(Label.TextProperty,
                       new Binding("Room_ID", BindingMode.OneWay, null, null, "Room: {0:d}"));
                    Label download = new Label();
                    download.SetBinding(Label.TextProperty,
                        new Binding("download", BindingMode.OneWay, null, null, "Download: {0} Mbps"));
                    Label upload = new Label();
                    upload.SetBinding(Label.TextProperty,
                        new Binding("upload", BindingMode.OneWay, null, null, "Upload: {0} Mbps"));
                    Label ping = new Label();
                    ping.SetBinding(Label.TextProperty,
                        new Binding("latency", BindingMode.OneWay, null, null, "Ping: {0:d} ms"));
                    
                    Label jitter = new Label();
                    jitter.SetBinding(Label.TextProperty,
                        new Binding("jitter", BindingMode.OneWay, null, null, "Jitter: {0} "));
                    jitter.SetBinding(Label.IsVisibleProperty, "isVisible");

                    Label maxDownload = new Label();
                    maxDownload.SetBinding(Label.TextProperty,
                        new Binding("maxDownload", BindingMode.OneWay, null, null, "Max Download: {0} Mbps"));
                    maxDownload.SetBinding(Label.IsVisibleProperty, "isVisible");

                    Label maxUpload = new Label();
                    maxUpload.SetBinding(Label.TextProperty,
                        new Binding("maxUpload", BindingMode.OneWay, null, null, "Max Upload: {0} Mbps"));
                    maxUpload.SetBinding(Label.IsVisibleProperty, "isVisible");

                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation.Horizontal,
                            Children =
                            {
                                new StackLayout
                                {
                                    VerticalOptions = LayoutOptions.Center,
                                    Spacing = 0,
                                    Children =
                                    {
                                        roomLabel,
                                        download,
                                        upload,
                                        ping,
                                        jitter,
                                        maxDownload,
                                        maxUpload
                                    }
                                }
                            }
                        }
                    };
                })
            };

            listview.ItemTapped += OnItemTapped;

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
