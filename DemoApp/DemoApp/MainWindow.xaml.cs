using Microsoft.Web.WebView2.Core;
using System;
using System.Windows;
using System.IO;

namespace DemoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeWebView();
        }

        async void InitializeWebView()
        {
            webView.NavigationStarting += EnsureHttps;


            await webView.EnsureCoreWebView2Async();
            webView.CoreWebView2.Navigate("https://www.facebook.com");

            string fileName = $"{Environment.CurrentDirectory}\\youtube.html";
            if (File.Exists(fileName))
            {
                webView.Source = new Uri($"file://{fileName}");
            }

            //webView.CoreWebView2.WebMessageReceived += webView2_WebMessageReceived;

            // "file://" is a URL prefix that specifies that the file being accessed is a local file.
            //webView.CoreWebView2.Navigate(Path.Combine("file://", Directory.GetCurrentDirectory(), "html", "index.html"));
        }

        void EnsureHttps(object sender, CoreWebView2NavigationStartingEventArgs args)
        {
            String uri = args.Uri;
            if (!uri.StartsWith("https://"))
            {
                webView.CoreWebView2.ExecuteScriptAsync($"alert('{uri} is not safe, try an https link')");
                args.Cancel = true;
            }
        }

        private void ButtonGo_Click(object sender, RoutedEventArgs e)
        {
            if (webView != null && webView.CoreWebView2 != null)
            {
                // Prepend "http://" if the input string does not contain a protocol.
                if (!addressBar.Text.Contains("://"))
                {
                    addressBar.Text = "http://" + addressBar.Text;
                }

                if (Uri.TryCreate(addressBar.Text, UriKind.Absolute, out Uri result))
                {
                    webView.CoreWebView2.Navigate(result.ToString());
                }
                else
                {
                    // Display an error message to the user indicating that the URL is invalid.
                    MessageBox.Show("Invalid URL.");
                }
            }
        }

        private void Button_Click_backward(object sender, RoutedEventArgs e)
        {
            if (webView.CanGoBack)
            {
                webView.GoBack();
            }
        }

        private void Button_Click_forward(object sender, RoutedEventArgs e)
        {
            if (webView.CanGoForward)
            {
                webView.GoForward();
            }
        }

        private void Button_Click_refresh(object sender, RoutedEventArgs e)
        {
            webView.CoreWebView2.Reload();
        }

        private void Button_Click_fontColor(object sender, RoutedEventArgs e)
        {
            webView.CoreWebView2.ExecuteScriptAsync("document.getElementById('headerText').style.color = 'red'");
        }

        private void Button_Click_headerText(object sender, RoutedEventArgs e)
        {
            webView.CoreWebView2.ExecuteScriptAsync("document.getElementById('headerText').innerText = 'HELLO WEBVIEW2 !!!'");
        }

        //public void webView2_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        //{
        //    string webMessage = e.TryGetWebMessageAsString();
        //    MessageBox.Show(webMessage);

        //    switch (webMessage)
        //    {
        //        case "beans":
        //            UpdateLabelContent("beans");
        //            break;

        //        case "rice":
        //            UpdateLabelContent("rice");
        //            break;

        //        case "melon":
        //            UpdateLabelContent("melon");
        //            break;

        //        default:
        //            break;
        //    }
        //}

        //private void UpdateLabelContent(string foodName)
        //{
        //    string content = $"{foodName} for you my friend";
        //    label1.Content = content;
        //}


    }
}
