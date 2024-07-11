using Firebase.Database;
using SmartGolfGlove_V2.Models;
using System.Diagnostics;

namespace SmartGolfGlove_V2.Views;

public partial class AboutUs : ContentPage
{
    public class Item
    {
        public string _title { get; set; }
    }
    public List<Item> _items;
    public AboutUs(FirebaseClient firebaseClient)
	{
		InitializeComponent();
        _items = new List<Item>();
        BindingContext = this;
        Client.FirebaseClient = firebaseClient;
        WelcomeUser.Text = "Welcome " + Client.childName + "\nEvaluate Your Game\nEvaluate Your Life";
    }
    public async Task LoadDataAsync()
    {
        var result = Client.FirebaseClient.Child(Client.childName).AsObservable<ClientDB>().Subscribe((item) =>
        {
            if (item.Object != null)
            {
                Client.clientDBList.Add(item.Object);
                _items.Add(new Item { _title = item.Object.dateTime });
            }
        });
    }
    private void CheckCallBack(object sender, EventArgs e)
    {
        cliCollection.ItemsSource = _items;
    }
    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        await LoadDataAsync();
    }
}