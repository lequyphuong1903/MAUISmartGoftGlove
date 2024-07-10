using SmartGolfGlove_V2.Models;
using SmartGolfGlove_V2.ViewModels;
using Syncfusion.Maui.Charts;
using System.Diagnostics;
using static Android.Provider.CalendarContract;

namespace SmartGolfGlove_V2.Views;

public partial class Personal : ContentPage
{
    private static Personal _instance;
    public static Personal Instance
    {
        get { return _instance ?? (_instance = new Personal()); }
    }
    public Personal()
	{
		InitializeComponent();
	}
	public void DrawIt()
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
            Debug.WriteLine("11111111111");
            lineCha.Series.Add(phiSeries);
            lineCha.Series.Add(thetaSeries);
            lineCha.Series.Add(yawSeries);
            Debug.WriteLine("22222222222");
        });
    }
    private void ConnectCallback(object sender, EventArgs e)
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
            Debug.WriteLine("11111111111");
            lineCha.Series.Add(phiSeries);
            lineCha.Series.Add(thetaSeries);
            lineCha.Series.Add(yawSeries);
            Debug.WriteLine("22222222222");
        });
    }
}