using SmartGolfGlove_V2.Models;
using SmartGolfGlove_V2.ViewModels;
using Syncfusion.Maui.Charts;
using System.Diagnostics;

namespace SmartGolfGlove_V2.Views;

public partial class Personal : ContentPage
{
    private static Personal _instance;
    private bool isTmrRunning = false;
    private int count = 0;
    public static Personal Instance
    {
        get { return _instance ?? (_instance = new Personal()); }
    }
    public Personal()
	{
		InitializeComponent();
	}
    private void SaveCallback(object sender, EventArgs e)
    {

    }
    private void StartCallback(object sender, EventArgs e)
    {
        isTmrRunning = true;
        Thread thread = new Thread(timer);
        thread.Start();
        Debug.WriteLine("Start timer here");
    }
    private void DrawIt()
    {
        for (int i = 0; i < 700; i++)
        {
            DataViewModel.AddValue(i * 0.004f, MessagePackage.phi[i] - MessagePackage.phi[0], MessagePackage.theta[i] - MessagePackage.theta[0], MessagePackage.yaw[i] - MessagePackage.yaw[0]);
        }
        LineSeries phiSeries = new LineSeries()
        {
            ItemsSource = DataViewModel.Data,
            XBindingPath = "Time",
            YBindingPath = "Phi",
            ShowMarkers = false,
            MarkerSettings = ChartMarker.phiMarker,
            IsVisibleOnLegend = true,
        };
        LineSeries thetaSeries = new LineSeries()
        {
            ItemsSource = DataViewModel.Data,
            XBindingPath = "Time",
            YBindingPath = "Theta",
            ShowMarkers = false,
            MarkerSettings = ChartMarker.thetaMarker,
            IsVisibleOnLegend = true,
        };
        LineSeries yawSeries = new LineSeries()
        {
            ItemsSource = DataViewModel.Data,
            XBindingPath = "Time",
            YBindingPath = "Yaw",
            ShowMarkers = false,
            MarkerSettings = ChartMarker.yawMarker,
            IsVisibleOnLegend = true,
        };
        MainThread.BeginInvokeOnMainThread(() =>
        {
            lineCha.Series.Add(phiSeries);
            lineCha.Series.Add(thetaSeries);
            lineCha.Series.Add(yawSeries);
            readyLb.Text = "FINISH";
        });
    }
    private void timer()
    {
        if (!isTmrRunning)
        {
            return;
        }
        DataViewModel.ResetData();
        MainThread.BeginInvokeOnMainThread(() =>
        {
            lineCha.Series.Clear();
            readyLb.Text = "READY";
        });
        Thread.Sleep(1000);
        MainThread.BeginInvokeOnMainThread(() =>
        {
            readyLb.Text = "1";
        });
        Thread.Sleep(1000);
        MainThread.BeginInvokeOnMainThread(() =>
        {
            readyLb.Text = "2";
        });
        Thread.Sleep(1000);
        MainThread.BeginInvokeOnMainThread(() =>
        {
            readyLb.Text = "3";
        });
        Thread.Sleep(1000);
        MainThread.BeginInvokeOnMainThread(() =>
        {
            readyLb.Text = "PLAY";
        });
        BLE.isTransfer = true;
        Thread.Sleep(3000);
        Thread DrawThread = new Thread(new ThreadStart(DrawIt));
        DrawThread.Start();

        isTmrRunning = false;
    }
}