using System.Collections.Generic;
using System.Threading.Tasks;

public class Client
{
    public Client()
    {
        this.Tcp = new TCP();
    }

    public delegate Task PacketHandler(Packet packet);

    public int DataBufferSize { get; set; } = 4096;

    public int Id { get; set; } = 0;

    public string Ip { get; set; } = "192.168.1.43";

    public bool IsConnected { get; set; } = false;

    public Dictionary<int, PacketHandler> PacketHandlers { get; set; }

    public int Port { get; set; } = 8000;

    public TCP Tcp { get; set; }
}