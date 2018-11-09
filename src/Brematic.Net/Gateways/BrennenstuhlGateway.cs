using Brematic.Net.Devices;

namespace Brematic.Net.Gateways
{
    public class BrennenstuhlGateway : Gateway
    {
        public BrennenstuhlGateway(string ipAddress) : this(ipAddress, 49880) { }

        public BrennenstuhlGateway(string ipAddress, int port) : base(ipAddress, port) { }
        
        protected internal override string GetHead(Device device)
        {
            return $"TXP:0,0,{device.Repeat},{device.PauseBS},{device.Tune},{device.BaudBS},";
        }

        protected internal override string GetTail(Device device)
        {
            return $"{device.TxVersion},{device.SpeedBS};";
        }
    }
}