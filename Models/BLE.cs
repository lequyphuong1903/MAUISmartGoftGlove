using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;

namespace SmartGolfGlove_V2.Models
{
    public class BLE
    {
        public static List<IDevice> deviceList { get; set; }
        public static IDevice connectedDevice { get; set; }
        public static IService service { get; set; }
        public static ICharacteristic characteristic { get; set; }
        public static IBluetoothLE ble { get; set; }
        public static IAdapter adapter { get; set; }
        public static BluetoothState state { get; set; }
        public static bool isConnected { get; set; }
        public static bool isTransfer {get; set;}
        static BLE()
        {
            deviceList = new List<IDevice>();
            ble = CrossBluetoothLE.Current;
            adapter = CrossBluetoothLE.Current.Adapter;
            state = ble.State;
            isConnected = false;
            isTransfer = false;
    }
    }
}
