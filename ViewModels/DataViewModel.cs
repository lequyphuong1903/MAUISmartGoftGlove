using SmartGolfGlove_V2.Models;

namespace SmartGolfGlove_V2.ViewModels
{
    public class DataViewModel
    {
        static public List<DataModel> Data { get; set; }   
        static DataViewModel()
        {
            Data = new List<DataModel>();
        }
        static public void AddValue(float time, float phi, float theta, float yaw)
        {
            Data.Add(new DataModel() { Time = time, Phi = phi, Theta = theta, Yaw = yaw });
        }
        static public void ResetData()
        {
            Data.Clear();
        }
    }
}
