using System.Net;
using System.Net.Sockets;
using System.Text;
using Brematic.Net.Devices;

namespace Brematic.Net.Gateways
{
    public abstract class Gateway
    {
        protected Gateway(string ipAddress, int port)
        {
            IPAddress = ipAddress;
            Port = port;
        }

        private string IPAddress { get; }

        private int Port { get; }

        protected internal abstract string GetHead(Device device);
        protected internal abstract string GetTail(Device device);

        public void SendRequest(Device device, DeviceAction action)
        {
            var head = GetHead(device);
            var tail = GetTail(device);

            var data = device.GetSignal(head, tail, action);
            SendDataToSocket(data);
        }

        private Socket ConnectSocket()
        {
            var hostEntry = Dns.GetHostEntry(IPAddress);

            foreach (var address in hostEntry.AddressList)
            {
                var ipEndPoint = new IPEndPoint(address, Port);
                var tempSocket = new Socket(SocketType.Dgram, ProtocolType.Udp);
                tempSocket.Connect(ipEndPoint);

                if (tempSocket.Connected) return tempSocket;
            }

            return null;
        }

        protected void SendDataToSocket(string data)
        {
            var byteData = Encoding.UTF8.GetBytes(data);
            using (var socket = ConnectSocket())
            {
                socket.Send(byteData);
            }
        }
    }
}