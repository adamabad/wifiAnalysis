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

            var dismissButton = new Button { Text = "Close Tips" };

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
                        Text = "Download speed - the speed of getting information from the web to your computer"
                    },
                    new Label
                    {
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        Text = "Upload speed - the speed of giving information from your computer to the web"
                    },
                    new Label
                    {
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        Text = "Jitter - the variation in times for packets to travel across the network"
                    },
                    new Label
                    {
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        Text = "Latency -  the time it takes for a data packet to travel across a network from one point on the network to another"
                    },
                    new Label
                    {
                        FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                        FontAttributes = FontAttributes.Bold,
                        Text = "Troubleshooting Tips"
                    },
                    new Label
                    {
                        FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                        Text = "If possible, try to place your wireless router in a central location in your home."
                    },
                    new Label
                    {
                        FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                        Text = "Thick walls (such as brick) can impede wireless signal strength transmission, reduing performance and interrupting network connectivity."
                    },
                    new Label
                    {
                        FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                        Text = "Reboot your wireless router and modems occasionally (at least once a week) to maintain network performance."
                    },
                    new Label
                    { 
                        FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                        Text = "For better stability, internet-connected devices should be far away from other electronics such as TVs, microwaves, and other connected devices."
                    },
                    new Label
                    {
                        FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                        Text = "If you are still not achieving your advertised download/upload speeds, contact your Internet Service Provider for more information regarding your internet plan."
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
