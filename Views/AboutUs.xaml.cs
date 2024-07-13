using Firebase.Database;
using Microcharts;
using SmartGolfGlove_V2.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace SmartGolfGlove_V2.Views;

public partial class AboutUs : ContentPage
{
    FirebaseClient firebaseClient = new FirebaseClient(baseUrl: "https://wristmotionofglove-default-rtdb.firebaseio.com/");
    public static ObservableCollection<ClientDB> clientDBList { get; set; } = new ObservableCollection<ClientDB>();
    public class Item
    {
        public string title { get; set; }
    }
    public List<Item> items = new List<Item>();
    public AboutUs()
	{
		InitializeComponent();
        BindingContext = this;
        WelcomeUser.Text = "Welcome " + "Phuong Lee" + "\nEvaluate Your Game\nEvaluate Your Life";
        var result = firebaseClient.Child(Client.childName).AsObservable<ClientDB>().Subscribe((item) =>
        {
            if (item.Object != null)
            {
                clientDBList.Add(item.Object);
                items.Add(new Item { title = item.Object.dateTime });
                Debug.Write("HERE");
            }
        });
    }
    public async Task LoadDataAsync()
    {

        
    }
    private async void UpdateCollection()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            cliCollection.ItemsSource = items;
        });
    }
    private async void CheckCallBack(object sender, EventArgs e)
    {
        //Thread thread = new Thread(new ThreadStart(LoadDataAsync));
        //thread.Start();
        await Task.Run(LoadDataAsync);
        await Task.Run(UpdateCollection);
    }

}