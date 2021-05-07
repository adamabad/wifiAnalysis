using System.Collections.Generic;
using Xamarin.Forms;

namespace wifiAnalysis
{
    class PreviousPage : ContentPage
    {
        public PreviousPage()
        {
            Title = "Previous Scan Results";
            ListView listview;
            Label header = new Label
            {
                Text = "Previous Scans",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            List<ScanObject> scansTest = new List<ScanObject>
            {

            };

            listview = new ListView
            {

                RowHeight = 120,
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
                                        ping
                                    }
                                }
                            }
                        }
                    };
                })
            };

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
