using AndroidX.Lifecycle;
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
    List<IDevice> deviceList = new List<IDevice>();
    private IDevice connectedDevice;
    private IService service;
    private ICharacteristic characteristic;
    public bool isConnected = false;
    private bool transfer = false;
    private int head = 0;

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
        
        var ble = CrossBluetoothLE.Current;
        var adapter = CrossBluetoothLE.Current.Adapter;
        var state = ble.State;

        adapter.DeviceDiscovered += (s, a) => deviceList.Add(a.Device);
        await adapter.StartScanningForDevicesAsync();
        for (int i = 0; i < deviceList.Count; i++)
        {
            if (deviceList[i].Name == "O-Glove")
            {
                Debug.WriteLine(deviceList[i].Name);
                try
                {
                    connectedDevice = deviceList[i];
                    await connectedDevice.RequestMtuAsync(180);
                    await adapter.ConnectToDeviceAsync(connectedDevice);
                    isConnected = true;
                }
                catch (DeviceConnectionException ex)
                {
                    // ... could not connect to device
                }
            }       
        }
        
        if (isConnected)
        {
            ConnectBtn.Text = connectedDevice.Name;
            ConnectBtn.IsEnabled = false;
            service = await connectedDevice.GetServiceAsync(Guid.Parse("4fafc201-1fb5-459e-8fcc-c5c9c331914b"));
            characteristic = await service.GetCharacteristicAsync(Guid.Parse("beb5483e-36e1-4688-b7f5-ea07361b26a8"));
            characteristic.ValueUpdated += (o, args) =>
            {
                if (transfer)
                {
                    var bytes = args.Characteristic.Value;
                    MessagePackage.phi[head] = BitConverter.ToSingle(bytes, 0);
                    MessagePackage.theta[head] = BitConverter.ToSingle(bytes, 4);
                    MessagePackage.yaw[head] = BitConverter.ToSingle(bytes, 8);
                    head++;
                    
                    if (head>=700)
                    {
                        transfer = false;
                    }
                    
                }
                
            };
            transfer = true;
            await characteristic.StartUpdatesAsync();
        }
    }
}
