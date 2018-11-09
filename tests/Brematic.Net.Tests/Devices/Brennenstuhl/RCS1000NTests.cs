using Brematic.Net.Devices;
using Brematic.Net.Devices.Brennenstuhl;
using Xunit;

namespace Brematic.Net.Tests.Devices.Brennenstuhl
{
    public class RCS1000NTests
    {
        [Theory]
        // Brennenstuhl
        [InlineData(DeviceAction.On,
            "TXP:0,0,10,5600,350,25,",
            "1,16;",
            "TXP:0,0,10,5600,350,25,1,3,1,3,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,1,3,1,3,3,1,1,3,3,1,1,3,1,3,1,3,3,1,1,16;"
        )]
        [InlineData(DeviceAction.Off,
            "TXP:0,0,10,5600,350,25,",
            "1,16;",
            "TXP:0,0,10,5600,350,25,1,3,1,3,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,1,3,1,3,3,1,1,3,3,1,1,3,3,1,1,3,1,3,1,16;"
        )]
        // Intertechno
        [InlineData(DeviceAction.On,
            "0,0,10,11200,350,26,0,",
            "1,32,0",
            "0,0,10,11200,350,26,0,1,3,1,3,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,1,3,1,3,3,1,1,3,3,1,1,3,1,3,1,3,3,1,1,32,0"
        )]
        [InlineData(DeviceAction.Off,
            "0,0,10,11200,350,26,0,",
            "1,32,0",
            "0,0,10,11200,350,26,0,1,3,1,3,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,1,3,1,3,3,1,1,3,3,1,1,3,3,1,1,3,1,3,1,32,0"
        )]
        public void GetSignal_Test(DeviceAction action, string head, string tail, string expectedResult)
        {
            // given
            var device = new RCS1000N("10000", "00100");

            // when
            var result = device.GetSignal(head, tail, action);

            // then
            Assert.Equal(expectedResult, result);
        }
    }
}