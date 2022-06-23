using Zenject;

public class GameSendBusiness
{
    [Inject]
    private readonly Client client;

    [Inject]
    private readonly TCPCommunicationBusiness tcpCommunicationBusiness;

    public void AskForGame()
    {
        int packetId = (int) ClientPackets.AskForGame;

        using (Packet packet = new Packet(packetId))
        {
            packet.Write(this.client.Id);
            this.tcpCommunicationBusiness.SendTCPData(packet);
        }
    }

    public void AskForPrivateGame()
    {
        int packetId = (int) ClientPackets.AskForPrivateGame;

        using (Packet packet = new Packet(packetId))
        {
            packet.Write(this.client.Id);
            this.tcpCommunicationBusiness.SendTCPData(packet);
        }
    }

    public void AskJoinPrivateGame(int lobbyCode)
    {
        int packetId = (int) ClientPackets.AskJoinPrivateGame;

        using (Packet packet = new Packet(packetId))
        {
            packet.Write(this.client.Id);
            packet.Write(lobbyCode);
            this.tcpCommunicationBusiness.SendTCPData(packet);
        }
    }

    public void CancelAskForGame()
    {
        int packetId = (int) ClientPackets.CancelAskForGame;

        using (Packet packet = new Packet(packetId))
        {
            packet.Write(this.client.Id);
            this.tcpCommunicationBusiness.SendTCPData(packet);
        }
    }

    public void LoadGameDone()
    {
        int packetId = (int) ClientPackets.LoadGameDone;

        using (Packet packet = new Packet(packetId))
        {
            packet.Write(this.client.Id);
            this.tcpCommunicationBusiness.SendTCPData(packet);
        }
    }
}