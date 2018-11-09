using Brematic.Net.Devices.Brennenstuhl;
using Brematic.Net.Devices.Elro;
using Brematic.Net.Devices.Intertechno;
using Brematic.Net.Gateways;
using Xunit;

namespace Brematic.Net.Tests.Gateways
{
    public class BrennenstuhlGatewayTests
    {
        private readonly BrennenstuhlGateway _brennenstuhlGateway;

        public BrennenstuhlGatewayTests()
        {
            _brennenstuhlGateway = new BrennenstuhlGateway("192.168.178.213");
        }

        [Fact]
        public void GetHead_RCS1000N_Test()
        {
            // given
            var device = new RCS1000N("00001", "10000");

            // when
            var result = _brennenstuhlGateway.GetHead(device);

            // then
            Assert.Equal("TXP:0,0,10,5600,350,25,", result);
        }

        [Fact]
        public void GetTail_RCS1000N_Test()
        {
            // given
            var device = new RCS1000N("00001", "10000");

            // when
            var result = _brennenstuhlGateway.GetTail(device);

            // then
            Assert.Equal("1,16;", result);
        }

        [Fact]
        public void GetHead_RCR1000N_Test()
        {
            // given
            var device = new RCR1000N("00001", "10000");

            // when
            var result = _brennenstuhlGateway.GetHead(device);

            // then
            Assert.Equal("TXP:0,0,10,5600,350,25,", result);
        }

        [Fact]
        public void GetTail_RCR1000N_Test()
        {
            // given
            var device = new RCR1000N("00001", "10000");

            // when
            var result = _brennenstuhlGateway.GetTail(device);

            // then
            Assert.Equal("1,16;", result);
        }

        [Fact]
        public void GetHead_AB440SA_Test()
        {
            // given
            var device = new AB440SA("00001", "10000");

            // when
            var result = _brennenstuhlGateway.GetHead(device);

            // then
            Assert.Equal("TXP:0,0,10,5600,350,25,", result);
        }

        [Fact]
        public void GetTail_AB440SA_Test()
        {
            // given
            var device = new AB440SA("00001", "10000");

            // when
            var result = _brennenstuhlGateway.GetTail(device);

            // then
            Assert.Equal("1,14;", result);
        }

        [Fact]
        public void GetHead_CMR1000_Test()
        {
            // given
            var device = new CMR1000("0000", "0010");

            // when
            var result = _brennenstuhlGateway.GetHead(device);

            // then
            Assert.Equal("TXP:0,0,6,11125,89,25,", result);
        }

        [Fact]
        public void GetTail_CMR1000_Test()
        {
            // given
            var device = new CMR1000("0000", "0010");

            // when
            var result = _brennenstuhlGateway.GetTail(device);

            // then
            Assert.Equal("1,140;", result);
        }
    }
}