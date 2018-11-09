using Brematic.Net.Devices;

namespace Brematic.Net.Gateways
{
    public class IntertechnoGateway : Gateway
    {
        public IntertechnoGateway(string ipAddress) : this(ipAddress, 49880) { }

        public IntertechnoGateway(string ipAddress, int port) : base(ipAddress, port) { }

        protected internal override string GetHead(Device device)
        {
            return $"0,0,{device.Repeat},{device.PauseIT},{device.Tune},{device.BaudIT},0,";
        }

        protected internal override string GetTail(Device device)
        {
            return $"{device.TxVersion},{device.SpeedIT},0";
        }
    }
}