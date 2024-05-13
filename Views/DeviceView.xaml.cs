using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Plugin.BLE.Abstractions.EventArgs;

namespace SmartGolfGlove.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class DeviceView : ContentPage
{
	IBluetoothLE ble;
	IAdapter adapter;
	ObservableCollection<IDevice> deviceList;
    IDevice device;
    public DeviceView()
	{
		InitializeComponent();
		ble = CrossBluetoothLE.Current;
		adapter = CrossBluetoothLE.Current.Adapter;
		deviceList = new ObservableCollection<IDevice>();
        lv.ItemsSource = deviceList;
    }
    private void btnStatus_Clicked(object sender, EventArgs e)
    {
        var state = ble.State;

        DisplayAlert("Notice", state.ToString(), "OK !");
        if (state == BluetoothState.Off)
        {
            txtErrorBle.BackgroundColor = Color.Red;
            txtErrorBle.TextColor = Color.White;
            txtErrorBle.Text = "Your Bluetooth is off ! Turn it on !";
        }
    }

    private async void btnScan_Clicked(object sender, EventArgs e)
    {
        try
        {
            deviceList.Clear();
            adapter.DeviceDiscovered += (s, a) =>
            {
                deviceList.Add(a.Device);
            };

            //We have to test if the device is scanning 
            if (!ble.Adapter.IsScanning)
            {
                await adapter.StartScanningForDevicesAsync();

            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Notice", ex.Message.ToString(), "Error !");
        }
    }
    private async void btnConnect_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (device != null)
            {
                await adapter.ConnectToDeviceAsync(device);

            }
            else
            {
                DisplayAlert("Notice", "No Device selected !", "OK");
            }
        }
        catch (DeviceConnectionException ex)
        {
            //Could not connect to the device
            DisplayAlert("Notice", ex.Message.ToString(), "OK");
        }
    }

    private async void btnKnowConnect_Clicked(object sender, EventArgs e)
    {

        try
        {
            await adapter.ConnectToKnownDeviceAsync(new Guid("guid"));

        }
        catch (DeviceConnectionException ex)
        {
            //Could not connect to the device
            DisplayAlert("Notice", ex.Message.ToString(), "OK");
        }
    }

    IList<IService> Services;
    IService Service;

    private async void btnGetServices_Clicked(object sender, EventArgs e)
    {
        Services = await device.GetServicesAsync();
        // Service = await device.GetServiceAsync(Guid.Parse("guid")); 
        //or we call the Guid of selected Device

        Service = await device.GetServiceAsync(device.Id);
    }

    IList<ICharacteristic> Characteristics;
    ICharacteristic Characteristic;

    private async void btnGetcharacters_Clicked(object sender, EventArgs e)
    {
        var characteristics = await Service.GetCharacteristicsAsync();
        Guid idGuid = Guid.Parse("guid");
        Characteristic = await Service.GetCharacteristicAsync(idGuid);
        //  Characteristic.CanRead
    }

    IDescriptor descriptor;
    IList<IDescriptor> descriptors;

    private async void btnDescriptors_Clicked(object sender, EventArgs e)
    {
        descriptors = await Characteristic.GetDescriptorsAsync();
        descriptor = await Characteristic.GetDescriptorAsync(Guid.Parse("guid"));

    }

    private async void btnDescRW_Clicked(object sender, EventArgs e)
    {
        var bytes = await descriptor.ReadAsync();
        await descriptor.WriteAsync(bytes);
    }

    private async void btnGetRW_Clicked(object sender, EventArgs e)
    {
        var bytes = await Characteristic.ReadAsync();
        await Characteristic.WriteAsync(bytes);
    }

    private async void btnUpdate_Clicked(object sender, EventArgs e)
    {
        Characteristic.ValueUpdated += (o, args) =>
        {
            var bytes = args.Characteristic.Value;
        };
        await Characteristic.StartUpdatesAsync();
    }

    private void lv_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (lv.SelectedItem == null)
        {
            return;
        }
        device = lv.SelectedItem as IDevice;
    }

    private void txtErrorBle_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {

    }
}
