using Brematic.Net.Gateways;

namespace Brematic.Net.Devices
{
    public abstract class Device
    {
        protected Device(string systemCode, string unitCode)
        {
            SystemCode = systemCode;
            UnitCode = unitCode;
        }

        protected string SystemCode { get; }

        protected string UnitCode { get; }

        public abstract string GetSignal(Gateway gateway, DeviceAction action);
    }
}