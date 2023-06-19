using Newtonsoft.Json;
using StockAnalyzer.Core.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Navigation;

namespace StockAnalyzer.Windows;

public partial class MainWindow : Window
{
    //private static string API_URL = "https://ps-async.fekberg.com/api/stocks";

    private static string API_URL = "https://api.polygon.io/v2/aggs/grouped/locale/us/market/stocks/2023-01-09?adjusted=true&apiKey=";
    private Stopwatch stopwatch = new Stopwatch();

    public MainWindow()
    {
        InitializeComponent();
    }



    private void Search_Click(object sender, RoutedEventArgs e)
    {
        BeforeLoadingStockData();

        var client = new WebClient();
        //var url = $"{API_URL}/{StockIdentifier.Text}";

        //var content = client.DownloadString($"{API_URL}/{StockIdentifier.Text}");
        var content = client.DownloadString($"{API_URL}");

        // Simulate that the web call takes a very long time
        Thread.Sleep(10000);

        var jsonSerializerSettings = new JsonSerializerSettings();
        jsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;

        //var data = JsonConvert.DeserializeObject<IEnumerable<StockPrice>>(content);
        var data = JsonConvert.DeserializeObject <PolygonStockPrice>(content, jsonSerializerSettings);

        Stocks.ItemsSource = data.results;

        AfterLoadingStockData();
    }








    private void BeforeLoadingStockData()
    {
        stopwatch.Restart();
        StockProgress.Visibility = Visibility.Visible;
        StockProgress.IsIndeterminate = true;
    }

    private void AfterLoadingStockData()
    {
        StocksStatus.Text = $"Loaded stocks for {StockIdentifier.Text} in {stopwatch.ElapsedMilliseconds}ms";
        StockProgress.Visibility = Visibility.Hidden;
    }

    private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        Process.Start(new ProcessStartInfo { FileName = e.Uri.AbsoluteUri, UseShellExecute = true });

        e.Handled = true;
    }

    private void Close_OnClick(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
}