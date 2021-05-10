using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace wifiAnalysis.View
{
    class TipsPage : ContentPage
    {
        public TipsPage()
        {
            Title = "Connectivity Tips";

            var dismissButton = new Button { Text = "Dismiss" };

            Content = new StackLayout
            {
                Padding = new Thickness(5),
                Children =
                {
                    new Label
                    {
                        FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                        FontAttributes = FontAttributes.Bold,
                        Text = "Definitions"
                    },
                    new Label
                    {
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        Text = "Download speed - refers to how many megabits of data per second the device can retrieve from a server in the form of images, videos, text, etc."
                    },
                    new Label
                    {
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        Text = "Upload speed - refers to how many megabits of data per second the device can send to a server (opposite of download)."
                    },
                    new Label
                    {
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        Text = "Jitter - a measure of the variation in the time between data packets arriving."
                    },
                    new Label
                    {
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        Text = "Latency - the amount of time it takes for a data packet to transfer to its destination."
                    },
                    new Label
                    {
                        FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                        FontAttributes = FontAttributes.Bold,
                        Text = "Troubleshooting tips"
                    },
                    new Label
                    {
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        Text = "Place your wireless router in a central location in your home."
                    },
                    new Label
                    {
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        Text = "Thick walls (such as brick) can impede wireless signal strength transmission."
                    },
                    new Label
                    {
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        Text = "Your modem and wireless router should be rebooted occasionally to maintain network performance."
                    },
                    new Label
                    {
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        Text = "If you are still not achieving your advertised download/upload speeds, please contact your Internet Service Provider (ISP) for more information about your internet plan."
                    },
                    dismissButton

                }
            };

            dismissButton.Clicked += OnDismissButtonClicked;

            async void OnDismissButtonClicked(object sender, EventArgs e)
            {
                await Navigation.PopModalAsync();
            }
        }

    }
}
