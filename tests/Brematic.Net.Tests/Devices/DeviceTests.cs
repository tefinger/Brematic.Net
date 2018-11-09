using System;
using System.Collections.Generic;
using System.Text;
using Brematic.Net.Devices;
using Brematic.Net.Exceptions;
using Xunit;

namespace Brematic.Net.Tests.Devices
{
    public class DeviceTests
    {
        private TestDevice device;

        public DeviceTests()
        {
            device = new TestDevice("", "");
        }

        [Theory]
        [InlineData("0", "1,3,3,1,")]
        [InlineData("1", "1,3,1,3,")]
        public void Encode_Test(string given, string expected)
        {
            // given

            // when
            var result = device.Encode(given);

            // then
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Encode_InvalidCode_Test()
        {           
            Assert.Throws<InvalidCodeException>(() => device.Encode("012"));
        }
    }

    class TestDevice : Device
    {
        public TestDevice(string systemCode, string unitCode) : base(systemCode, unitCode) { }
    }
}
