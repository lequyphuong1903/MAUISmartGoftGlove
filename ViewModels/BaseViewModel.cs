using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace SmartGolfGlove_V2.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private string _title;
    }
}
