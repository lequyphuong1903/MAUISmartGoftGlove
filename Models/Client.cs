using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;

namespace SmartGolfGlove_V2.Models
{
    public class Client
    {
        public static FirebaseClient FirebaseClient { get; set; }
        public static string childName { get; set; }
        static Client() 
        {
        }
        public static void OnSave()
        {
            FirebaseClient.Child(childName).PostAsync(new ClientDB
            {
                phi = MessagePackage.phi,
                theta = MessagePackage.theta,
                yaw = MessagePackage.yaw,
                dateTime = MessagePackage.dataTime,
            });
        }
    }
}
