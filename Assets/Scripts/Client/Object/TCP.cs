using System.Net.Sockets;

public class TCP
{
    public byte[] ReceiveBuffer { get; set; }

    public Packet ReceivedData { get; set; }

    public TcpClient Socket { get; set; }

    public NetworkStream Stream { get; set; }
}