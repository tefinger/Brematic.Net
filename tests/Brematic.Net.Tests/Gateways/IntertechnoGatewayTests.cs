using Brematic.Net.Devices.Brennenstuhl;
using Brematic.Net.Devices.Elro;
using Brematic.Net.Devices.Intertechno;
using Brematic.Net.Gateways;
using Xunit;

namespace Brematic.Net.Tests.Gateways
{
    public class IntertechnoGatewayTests
    {
        private readonly IntertechnoGateway _intertechnoGateway;

        public IntertechnoGatewayTests()
        {
            _intertechnoGateway = new IntertechnoGateway("192.168.178.213");
        }

        [Fact]
        public void GetHead_RCS1000N_Test()
        {
            // given
            var device = new RCS1000N("00001", "10000");

            // when
            var result = _intertechnoGateway.GetHead(device);

            // then
            Assert.Equal("0,0,10,11200,350,26,0,", result);
        }

        [Fact]
        public void GetTail_RCS1000N_Test()
        {
            // given
            var device = new RCS1000N("00001", "10000");

            // when
            var result = _intertechnoGateway.GetTail(device);

            // then
            Assert.Equal("1,32,0", result);
        }

        [Fact]
        public void GetHead_RCR1000N_Test()
        {
            // given
            var device = new RCR1000N("00001", "10000");

            // when
            var result = _intertechnoGateway.GetHead(device);

            // then
            Assert.Equal("0,0,10,11200,350,26,0,", result);
        }

        [Fact]
        public void GetTail_RCR1000N_Test()
        {
            // given
            var device = new RCR1000N("00001", "10000");

            // when
            var result = _intertechnoGateway.GetTail(device);

            // then
            Assert.Equal("1,32,0", result);
        }

        [Fact]
        public void GetHead_AB440SA_Test()
        {
            // given
            var device = new AB440SA("00001", "10000");

            // when
            var result = _intertechnoGateway.GetHead(device);

            // then
            Assert.Equal("0,0,10,11200,350,26,0,", result);
        }

        [Fact]
        public void GetTail_AB440SA_Test()
        {
            // given
            var device = new AB440SA("00001", "10000");

            // when
            var result = _intertechnoGateway.GetTail(device);

            // then
            Assert.Equal("1,32,0", result);
        }

        [Fact]
        public void GetHead_CMR1000_Test()
        {
            // given
            var device = new CMR1000("0000", "0010");

            // when
            var result = _intertechnoGateway.GetHead(device);

            // then
            Assert.Equal("0,0,6,11125,89,26,0,", result);
        }

        [Fact]
        public void GetTail_CMR1000_Test()
        {
            // given
            var device = new CMR1000("0000", "0010");

            // when
            var result = _intertechnoGateway.GetTail(device);

            // then
            Assert.Equal("1,125,0", result);
        }
    }
}