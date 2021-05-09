using Xamarin.Forms;
using System;

namespace wifiAnalysis
{
    class DetailPage : ContentPage
    {
        public DetailPage()
        {
            SetBinding(ContentPage.TitleProperty, new Binding("Room_Name"));
            Label header = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };
            header.SetBinding(Label.TextProperty, "Room_Name");
            var dateLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            };
            dateLabel.SetBinding(Label.TextProperty,
                new Binding("testDate", BindingMode.OneWay, new StringToDate(), null, "{0}"));
            var downloadLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };
            downloadLabel.SetBinding(Label.TextProperty,
                new Binding("download", BindingMode.OneWay, null, null, "{0} Mbps"));

            var uploadLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };
            uploadLabel.SetBinding(Label.TextProperty,
                new Binding("upload", BindingMode.OneWay, null, null, "{0} Mbps"));

            var hostnameLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };
            hostnameLabel.SetBinding(Label.TextProperty, "hostname");

            var ipLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };
            ipLabel.SetBinding(Label.TextProperty, "ip_address");

            var jitterLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };
            jitterLabel.SetBinding(Label.TextProperty,
                new Binding("jitter", BindingMode.OneWay, null, null, "{0:d} ms"));

            var pingLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };
            pingLabel.SetBinding(Label.TextProperty,
                new Binding("latency", BindingMode.OneWay, null, null, "{0:d} ms"));

            var maxDLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };
            maxDLabel.SetBinding(Label.TextProperty,
                new Binding("maxDownload", BindingMode.OneWay, null, null, "{0} Mbps"));

            var maxULabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };
            maxULabel.SetBinding(Label.TextProperty,
                new Binding("maxUpload", BindingMode.OneWay, null, null, "{0} Mbps"));

            var dismissButton = new Button { Text = "Dismiss" };
            dismissButton.Clicked += OnDismissButtonClicked;
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
                    dismissButton
                }
            };
        }
        
        async void OnDismissButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
