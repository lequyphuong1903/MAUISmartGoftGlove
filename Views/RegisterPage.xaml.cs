using SmartGolfGlove.ViewModels;
namespace SmartGolfGlove.Views;


[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
		BindingContext = new RegisterViewModel(Navigation);
	}
}
