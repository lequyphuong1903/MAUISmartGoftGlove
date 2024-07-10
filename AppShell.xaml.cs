using SmartGolfGlove_V2.ViewModels;
namespace SmartGolfGlove_V2
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NCaF5cXmZCdkx+WmFZfVpgfV9DaVZVQGY/P1ZhSXxXdkJjUH1ccHRURmddWEU=");
            this.BindingContext = new AppShellViewModel();
        }
    }
}
