using Newtonsoft.Json;

namespace SmartGolfGlove.Views;


[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Dashboard : ContentPage
{
    public Dashboard()
    {
        InitializeComponent();
        GetProfileInfo();
    }
    private void GetProfileInfo()
    {
        var userInfo = JsonConvert.DeserializeObject<Firebase.Auth.FirebaseAuth>(Preferences.Get("FreshFirebaseToken", ""));
        UserEmail.Text = userInfo.User.Email;
    }
}
