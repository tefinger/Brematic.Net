using System;
using Brematic.Net.Devices;

namespace Brematic.Net.Gateways
{
    public class IntertechnoGateway : Gateway
    {
        public IntertechnoGateway(string ipAddress) : this(ipAddress, 49880) { }

        public IntertechnoGateway(string ipAddress, int port) : base(ipAddress, port) { }
    }
}