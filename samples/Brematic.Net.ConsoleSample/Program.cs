using Brematic.Net.Devices;
using Brematic.Net.Devices.Brennenstuhl;
using Brematic.Net.Gateways;

namespace Brematic.Net.ConsoleSample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var systemCode = "01010";
            var unitCode = "00010";

            var device = new RCS1000N(systemCode, unitCode);

            var gateway = new BrennenstuhlGateway("192.168.178.213");

            gateway.SendRequest(device, DeviceAction.On);
        }
    }
}