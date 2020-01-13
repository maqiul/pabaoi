using System;
using System.Collections.Generic;
using PylonC.NET;
using System.Threading;

namespace pcbaoi.Tools
{
    /* Provides methods for listing all available devices. */
    public static class DeviceEnumerator
    {
        /* Data class used for holding device data. */
        public class Device
        {
            public string Name; /* The friendly name of the device. */
            public string UserDefinedName; /* The full name string which is unique. */
            public uint Index; /* The index of the device. */
            public string Tooltip; /* The displayed tooltip */

        }

        /* Queries the number of available devices and creates a list with device data. */
        public static List<Device> EnumerateDevices()
        {
            /* Create a list for the device data. */
            List<Device> list = new List<Device>();

            /* Enumerate all camera devices. You must call
            PylonEnumerateDevices() before creating a device. */
            uint count = PylonC.NET.Pylon.EnumerateDevices();

            /* Get device data from all devices. */
            for (uint i = 0; i < count; ++i)
            {
                /* Create a new data packet. */
                Device device = new Device();
                /* Get the device info handle of the device. */
                PYLON_DEVICE_INFO_HANDLE hDi = PylonC.NET.Pylon.GetDeviceInfoHandle(i);
                /* Get the name. */
                device.Name = PylonC.NET.Pylon.DeviceInfoGetPropertyValueByName(hDi, PylonC.NET.Pylon.cPylonDeviceInfoFriendlyNameKey);
                /* Get the serial number */
                device.UserDefinedName = PylonC.NET.Pylon.DeviceInfoGetPropertyValueByName(hDi, PylonC.NET.Pylon.cPylonDeviceInfoUserDefinedNameKey);
                /* Set the index. */
                device.Index = i;

                /* Create tooltip */
                string tooltip = "";
                uint propertyCount = PylonC.NET.Pylon.DeviceInfoGetNumProperties(hDi);

                if (propertyCount > 0)
                {
                    for (uint j = 0; j < propertyCount; j++)
                    {
                        tooltip += PylonC.NET.Pylon.DeviceInfoGetPropertyName(hDi, j) + ": " + PylonC.NET.Pylon.DeviceInfoGetPropertyValueByIndex(hDi, j);
                        if (j != propertyCount - 1)
                        {
                            tooltip += "\n";
                        }
                    }
                }
                device.Tooltip = tooltip;
                /* Add to the list. */
                list.Add(device);
            }
            return list;
        }
    }
}
