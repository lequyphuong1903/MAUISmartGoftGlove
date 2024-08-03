namespace SmartGolfGlove_V2.Models
{
    public static class MessagePackage
    {
        public static float[] phi;
        public static float[] theta;
        public static float[] yaw;
        public static float[] gyrTotal;
        public static string dataTime;
        static public int head { get; set; }
        static MessagePackage()
        {
            phi = new float[700];
            theta = new float[700];
            yaw = new float[700];
            gyrTotal = new float[700];
            head = 0;
            dataTime = "";
        }
    }
}
