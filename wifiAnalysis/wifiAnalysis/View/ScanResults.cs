using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace wifiAnalysis
{
    public class ScanResults : ContentPage
    {
        public ScanResults(ScanObject scanResult)
        {
            Title = "Scan Results";

            StackLayout testStack = new StackLayout
            {
                Padding = new Thickness(5, 10)
            };

            PropertyInfo[] properties = scanResult.GetType().GetProperties();
            foreach (var p in properties)
            {
                var myVal = p.GetValue(scanResult);

                var row = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Children =
                        {
                            new Label{ Text = $"{p.Name}:", FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)), HorizontalOptions = LayoutOptions.StartAndExpand },
                            new Label{ Text = $"{myVal}", FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)), HorizontalOptions = LayoutOptions.EndAndExpand }
                        }
                };

                testStack.Children.Add(row);
            }


            Button saveResultsButton = new Button
            {
                Text = "Save Results",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center
            };

            async void onSaveButtonClicked(object sender, EventArgs args)
            {
                await App.ScanDatabase.SaveScanAsync(scanResult);
                saveResultsButton.Text = "Results Saved!";
                saveResultsButton.IsEnabled = false;
                await Navigation.PushAsync(new PreviousPage());
            }

            saveResultsButton.Clicked += onSaveButtonClicked;

            testStack.Children.Add(saveResultsButton);

            // Build the page.
            this.Content = testStack;
        }
    }
}
