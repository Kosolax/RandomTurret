using Newtonsoft.Json;

using Zenject;

public class SessionSendBusiness
{
    [Inject]
    private readonly Client client;

    [Inject]
    private readonly Player player;

    [Inject]
    private readonly TCPCommunicationBusiness tcpCommunicationBusiness;

    public void WelcomeReceived()
    {
        int packetId = 0;
        if (ConnectionManager.Instance != null && ConnectionManager.Instance.IsDoingConnection)
        {
            packetId = (int) ClientPackets.AskingConnection;
        }
        else if (InscriptionManager.Instance != null && InscriptionManager.Instance.IsDoingRegister)
        {
            packetId = (int) ClientPackets.AskingRegister;
        }

        using (Packet packet = new Packet(packetId))
        {
            packet.Write(this.client.Id);
            packet.Write(JsonConvert.SerializeObject(this.player));

            this.tcpCommunicationBusiness.SendTCPData(packet);
        }
    }
}