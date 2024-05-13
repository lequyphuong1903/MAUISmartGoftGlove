using SmartGolfGlove.ViewModels;
namespace SmartGolfGlove
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel(Navigation);
        }

    }

}
