using Firebase.Database;
using SmartGolfGlove_V2.Models;
using System.Collections.ObjectModel;
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
        LoadDataAsync();
    }
    public async void LoadDataAsync()
    {
        var result = Client.FirebaseClient.Child(Client.childName).AsObservable<ClientDB>().Subscribe((item) =>
        {
            Debug.Write(item.Key);
            Debug.Write(item.Object.dateTime);
            if (item.Object != null)
            {
                Client.clientDBList.Add(item.Object);
                _items.Add(new Item { _title = item.Object.dateTime });
                Debug.WriteLine("HEREEE");
            }
        });
    }
    private void CheckCallBack(object sender, EventArgs e)
    {
        Debug.WriteLine(Client.clientDBList[0].dateTime);
        cliCollection.ItemsSource = _items;
    }
}