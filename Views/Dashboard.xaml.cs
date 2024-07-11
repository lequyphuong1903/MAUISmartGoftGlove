using Newtonsoft.Json;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;
using SmartGolfGlove_V2.Models;
using System.Diagnostics;

namespace SmartGolfGlove_V2.Views;


[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Dashboard : ContentPage
{
    private static Dashboard _instance;
    public static Dashboard Instance
    {
        get { return _instance ?? (_instance = new Dashboard()); }
    }

    public Dashboard()
    {
        InitializeComponent();
        GetProfileInfo();
    }
    private async void GetProfileInfo()
    {
        var userInfo = JsonConvert.DeserializeObject<Firebase.Auth.FirebaseAuth>(Preferences.Get("FreshFirebaseToken", ""));
        WelcomeUser.Text = "Welcome " + userInfo.User.Email +"\nEvaluate Your Game\nEvaluate Your Life";
    }

    private async void ConnectCallback(object sender, EventArgs e)
    {
        BLE.adapter.DeviceDiscovered += (s, a) => BLE.deviceList.Add(a.Device);
        await BLE.adapter.StartScanningForDevicesAsync();
        for (int i = 0; i < BLE.deviceList.Count; i++)
        {
            if (BLE.deviceList[i].Name == "O-Glove")
            {
                Debug.WriteLine(BLE.deviceList[i].Name);
                try
                {
                    BLE.connectedDevice = BLE.deviceList[i];
                    await BLE.connectedDevice.RequestMtuAsync(180);
                    await BLE.adapter.ConnectToDeviceAsync(BLE.connectedDevice);
                    BLE.isConnected = true;
                    await DisplayAlert("Connection", "Connect Successfully", "OK");
                }
                catch (DeviceConnectionException ex)
                {
                    await DisplayAlert("Error", ex.Message, "OK");
                }
            }       
        }
        
        if (BLE.isConnected)
        {
            ConnectBtn.Text = BLE.connectedDevice.Name;
            ConnectBtn.IsEnabled = false;
            BLE.service = await BLE.connectedDevice.GetServiceAsync(Guid.Parse("4fafc201-1fb5-459e-8fcc-c5c9c331914b"));
            BLE.characteristic = await BLE.service.GetCharacteristicAsync(Guid.Parse("beb5483e-36e1-4688-b7f5-ea07361b26a8"));
            BLE.characteristic.ValueUpdated += (o, args) =>
            {
                if (BLE.isTransfer)
                {
                    var bytes = args.Characteristic.Value;
                    MessagePackage.phi[MessagePackage.head] = BitConverter.ToSingle(bytes, 0);
                    MessagePackage.theta[MessagePackage.head] = BitConverter.ToSingle(bytes, 4);
                    MessagePackage.yaw[MessagePackage.head] = BitConverter.ToSingle(bytes, 8);
                    MessagePackage.head++;
                    
                    if (MessagePackage.head >= 700)
                    {
                        MessagePackage.head = 0;
                        BLE.isTransfer = false;
                    }
                    
                }
                
            };
            await BLE.characteristic.StartUpdatesAsync();
        }
    }
}
