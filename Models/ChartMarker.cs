using Syncfusion.Maui.Charts;

namespace SmartGolfGlove_V2.Models
{
    public static class ChartMarker
    {
        public static ChartMarkerSettings phiMarker { get; }
        public static ChartMarkerSettings thetaMarker { get; }
        public static ChartMarkerSettings yawMarker { get; }
        static ChartMarker()
        {
            phiMarker = new ChartMarkerSettings();
            phiMarker.Type = ShapeType.Diamond;
            phiMarker.Fill = Colors.Red;

            thetaMarker = new ChartMarkerSettings();
            thetaMarker.Type = ShapeType.Circle;
            thetaMarker.Fill = Colors.Green;

            yawMarker = new ChartMarkerSettings();
            yawMarker.Type = ShapeType.Triangle;
            yawMarker.Fill = Colors.Blue;
        }
    }
}
