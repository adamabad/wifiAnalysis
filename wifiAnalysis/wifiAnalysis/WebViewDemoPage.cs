using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace wifiAnalysis
{
    public class WebViewDemoPage : ContentPage
    {
        public WebViewDemoPage()
        {
            Title = "Scan Settings";

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
                    Url = "http://klaipsc.mathcs.wilkes.edu/speedtest",
                },
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            Button saveResultsButton = new Button
            {
                Text = "Save Results",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center
            };

            async void onSaveButtonClicked(object sender, EventArgs args)
            {
                string result = await webView.EvaluateJavaScriptAsync("parseResults2()");
                object obj = null;
                if (result != null)
                {
                    obj = JsonConvert.DeserializeObject(result);
                }
                Console.WriteLine(obj!=null ? obj : "object is null");

                //insert values into DB

                //navigate to next page
            }

            saveResultsButton.Clicked += onSaveButtonClicked;

            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {
                    header,
                    webView,
                    saveResultsButton
                }
            };
        }
    }
}