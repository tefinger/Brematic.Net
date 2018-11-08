using Brematic.Net.Devices;

namespace Brematic.Net.Gateways
{
    public class BrennenstuhlGateway : Gateway
    {
        public BrennenstuhlGateway(string ipAddress) : this(ipAddress, 49880) { }

        public BrennenstuhlGateway(string ipAddress, int port) : base(ipAddress, port) { }
    }
}