using Zenject;

public class TowerSendBusiness
{
    [Inject]
    private readonly Client client;

    [Inject]
    private readonly TCPCommunicationBusiness tcpBusiness;

    public void InvokeTower()
    {
        int packetId = (int) ClientPackets.InvokeTower;

        using (Packet packet = new Packet(packetId))
        {
            packet.Write(this.client.Id);
            this.tcpBusiness.SendTCPData(packet);
        }
    }

    public void MergeTower(int draggedTowerIndex, int droppedTowerIndex)
    {
        int packetId = (int) ClientPackets.MergeTower;

        using (Packet packet = new Packet(packetId))
        {
            packet.Write(this.client.Id);
            packet.Write(draggedTowerIndex);
            packet.Write(droppedTowerIndex);
            this.tcpBusiness.SendTCPData(packet);
        }
    }
}