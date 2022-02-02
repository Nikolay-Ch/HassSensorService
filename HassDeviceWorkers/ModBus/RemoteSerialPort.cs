using System.Net.Sockets;

namespace HassDeviceWorkers.ModBus
{
    class RemoteSerialPort : ISerialPort
    {
        TcpClient TcpClient { get; set; }
        public int ReadTimeout
        {
            get => TcpClient.ReceiveTimeout;
            set
            {
                TcpClient.ReceiveTimeout = ReadTimeout;
                TcpClient.GetStream().ReadTimeout = value;
            }
        }

        public RemoteSerialPort(string hostname, int port)
        {
            TcpClient = new TcpClient(hostname, port);
        }

        public int Read(byte[] buffer, int offset, int count) =>
            TcpClient.GetStream().Read(buffer, 0, count);

        public void Write(byte[] buffer, int offset, int count) =>
            TcpClient.GetStream().Write(buffer, offset, count);

        public void Dispose() => TcpClient.Dispose();
    }
}
