using System.Net.Sockets;

namespace HassDeviceWorkers.ModBus
{
    class RemoteSerialPort(string hostname, int port) : ISerialPort
    {
        TcpClient TcpClient { get; set; } = new TcpClient(hostname, port);
        public int ReadTimeout
        {
            get => TcpClient.ReceiveTimeout;
            set
            {
                TcpClient.ReceiveTimeout = ReadTimeout;
                TcpClient.GetStream().ReadTimeout = value;
            }
        }

        public int Read(byte[] buffer, int offset, int count) =>
            TcpClient.GetStream().Read(buffer, 0, count);

        public void Write(byte[] buffer, int offset, int count) =>
            TcpClient.GetStream().Write(buffer, offset, count);

        public void Dispose() => TcpClient.Dispose();
    }
}
