using SmartGolfGlove_V2.ViewModels;

namespace SmartGolfGlove_V2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel(Navigation);
        }
    }

}
