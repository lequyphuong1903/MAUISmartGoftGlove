using Microsoft.Toolkit.Mvvm.Input;


namespace SmartGolfGlove_V2.ViewModels
{
    public partial class AppShellViewModel : BaseViewModel
    {
        [ICommand]
        async void SignOut()
        {
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
    }
}
