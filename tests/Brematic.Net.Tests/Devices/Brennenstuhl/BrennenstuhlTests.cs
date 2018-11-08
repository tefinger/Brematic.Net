using Brematic.Net.Devices;
using Brematic.Net.Devices.Brennenstuhl;
using Brematic.Net.Gateways;
using Xunit;

namespace Brematic.Net.Tests.Devices.Brennenstuhl
{
    public class BrennenstuhlTests
    {
        private readonly BrennenstuhlGateway _brennenstuhlGateway;
        private readonly IntertechnoGateway _intertechnoGateway;

        public BrennenstuhlTests()
        {
            _brennenstuhlGateway = new BrennenstuhlGateway("192.168.178.213");
            _intertechnoGateway = new IntertechnoGateway("192.68.178.213");
        }

        [Theory]
        [InlineData(DeviceAction.On,
            "TXP:0,0,10,5600,350,25,1,3,1,3,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,1,3,1,3,3,1,1,3,3,1,1,3,1,3,1,3,3,1,1,16;",
            "0,0,10,11200,350,26,0,1,3,1,3,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,1,3,1,3,3,1,1,3,3,1,1,3,1,3,1,3,3,1,1,32,0"
            )]
        [InlineData(DeviceAction.Off,
            "TXP:0,0,10,5600,350,25,1,3,1,3,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,1,3,1,3,3,1,1,3,3,1,1,3,3,1,1,3,1,3,1,16;",
            "0,0,10,11200,350,26,0,1,3,1,3,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,1,3,1,3,3,1,1,3,3,1,1,3,3,1,1,3,1,3,1,32,0")]
        public void RCS1000N_Test(DeviceAction action, string expectedResultBS, string expectedResultIT)
        {
            // given
            var device = new RCS1000N("10000", "00100");

            // when
            var resultBS = device.GetSignal(_brennenstuhlGateway, action);
            var resultIT = device.GetSignal(_intertechnoGateway, action);

            // then
            Assert.Equal(expectedResultBS, resultBS);
            Assert.Equal(expectedResultIT, resultIT);
        }

        [Theory]
        [InlineData(DeviceAction.On,
            "TXP:0,0,10,5600,350,25,1,3,1,3,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,1,3,1,3,3,1,1,3,3,1,1,3,1,3,1,3,3,1,1,16;",
            "0,0,10,11200,350,26,0,1,3,1,3,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,1,3,1,3,3,1,1,3,3,1,1,3,1,3,1,3,3,1,1,32,0"
        )]
        [InlineData(DeviceAction.Off,
            "TXP:0,0,10,5600,350,25,1,3,1,3,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,1,3,1,3,3,1,1,3,3,1,1,3,3,1,1,3,1,3,1,16;",
            "0,0,10,11200,350,26,0,1,3,1,3,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,1,3,1,3,3,1,1,3,3,1,1,3,3,1,1,3,1,3,1,32,0")]
        public void RCR1000N_Test(DeviceAction action, string expectedResultBS, string expectedResultIT)
        {
            // given
            var device = new RCR1000N("10000", "00100");

            // when
            var resultBS = device.GetSignal(_brennenstuhlGateway, action);
            var resultIT = device.GetSignal(_intertechnoGateway, action);

            // then
            Assert.Equal(expectedResultBS, resultBS);
            Assert.Equal(expectedResultIT, resultIT);
        }
    }
}