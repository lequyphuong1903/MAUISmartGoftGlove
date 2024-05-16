using SmartGolfGlove_V2.ViewModels;

namespace SmartGolfGlove_V2.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
        BindingContext = new RegisterViewModel(Navigation);
    }
}