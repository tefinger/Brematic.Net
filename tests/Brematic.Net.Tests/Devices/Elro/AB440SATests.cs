using Brematic.Net.Devices;
using Brematic.Net.Devices.Elro;
using Xunit;

namespace Brematic.Net.Tests.Devices.Elro
{
    public class AB440SATests
    {
        [Theory]
        // Brennenstuhl
        [InlineData(DeviceAction.On,
            "TXP:0,0,10,5600,350,25,",
            "1,14;",
            "TXP:0,0,10,5600,350,25,1,3,1,3,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,1,3,1,3,3,1,1,3,3,1,1,3,1,3,1,3,3,1,1,14;"
        )]
        [InlineData(DeviceAction.Off,
            "TXP:0,0,10,5600,350,25,",
            "1,14;",
            "TXP:0,0,10,5600,350,25,1,3,1,3,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,3,1,1,3,1,3,1,3,3,1,1,3,3,1,1,3,3,1,1,3,1,3,1,14;"
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
            var device = new AB440SA("10000", "00100");

            // when
            var result = device.GetSignal(head, tail, action);

            // then
            Assert.Equal(expectedResult, result);
        }
    }
}
