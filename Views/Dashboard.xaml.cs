using Newtonsoft.Json;
//using Plugin.BLE;
//using Plugin.BLE.Abstractions.Contracts;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace SmartGolfGlove_V2.Views;


[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Dashboard : ContentPage
{
    public Dashboard()
    {
        InitializeComponent();
        GetProfileInfo();
    }
    private async void GetProfileInfo()
    {
        var userInfo = JsonConvert.DeserializeObject<Firebase.Auth.FirebaseAuth>(Preferences.Get("FreshFirebaseToken", ""));
        UserEmail.Text = userInfo.User.Email;

    }
}
