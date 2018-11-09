using Brematic.Net.Devices;
using Brematic.Net.Devices.Intertechno;
using Brematic.Net.Exceptions;
using Xunit;

namespace Brematic.Net.Tests.Devices.Intertechno
{
    public class CMR1000Tests
    {
        [Theory]
        // Brennenstuhl
        [InlineData(DeviceAction.On,
            "TXP:0,0,6,11125,89,25,",
            "1,140;",
            "TXP:0,0,6,11125,89,25,4,12,4,12,4,12,4,12,4,12,4,12,4,12,4,12,4,12,4,12,4,12,4,12,4,12,12,4,4,12,4,12,4,12,4,12,4,12,12,4,4,12,12,4,4,12,12,4,1,140;"
        )]
        [InlineData(DeviceAction.Off,
            "TXP:0,0,6,11125,89,25,",
            "1,140;",
            "TXP:0,0,6,11125,89,25,4,12,4,12,4,12,4,12,4,12,4,12,4,12,4,12,4,12,4,12,4,12,4,12,4,12,12,4,4,12,4,12,4,12,4,12,4,12,12,4,4,12,12,4,4,12,4,12,1,140;"
        )]
        // Intertechno
        [InlineData(DeviceAction.On,
            "0,0,6,11125,89,26,0,",
            "1,125,0",
            "0,0,6,11125,89,26,0,4,12,4,12,4,12,4,12,4,12,4,12,4,12,4,12,4,12,4,12,4,12,4,12,4,12,12,4,4,12,4,12,4,12,4,12,4,12,12,4,4,12,12,4,4,12,12,4,1,125,0"
        )]
        [InlineData(DeviceAction.Off,
            "0,0,6,11125,89,26,0,",
            "1,125,0",
            "0,0,6,11125,89,26,0,4,12,4,12,4,12,4,12,4,12,4,12,4,12,4,12,4,12,4,12,4,12,4,12,4,12,12,4,4,12,4,12,4,12,4,12,4,12,12,4,4,12,12,4,4,12,4,12,1,125,0"
        )]
        public void GetSignal_Test(DeviceAction action, string head, string tail, string expectedResult)
        {
            // given
            var device = new CMR1000("0000", "0010");

            // when
            var result = device.GetSignal(head, tail, action);

            // then
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Encode_InvalidCode_Test()
        {           
            var device = new CMR1000("0000", "0010");

            Assert.Throws<InvalidCodeException>(() => device.Encode("012"));
        }
    }
}
