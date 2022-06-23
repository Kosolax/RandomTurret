using System.Collections.Generic;

using Zenject;

using static Client;

public class ClientBusiness
{
    [Inject]
    private readonly Client client;

    [Inject]
    private readonly GameHandleBusiness gameHandleBusiness;

    [Inject]
    private readonly GamerHandleBusiness gamerHandleBusiness;

    [Inject]
    private readonly MobHandleBusiness mobHandleBusiness;

    [Inject]
    private readonly Player player;

    [Inject]
    private readonly SessionHandleBusiness sessionHandleBusiness;

    [Inject]
    private readonly TCPCommunicationBusiness tcpCommunicationBusiness;

    [Inject]
    private readonly TowerHandleBusiness towerHandleBusiness;

    public void ConnectionToServer()
    {
        ConnectionManager.Instance.IsDoingConnection = true;
        this.player.Mail = ConnectionManager.Instance.MailField.text;
        this.player.Password = ConnectionManager.Instance.PasswordField.text;
        ConnectionManager.Instance.ConnectButton.interactable = false;
        this.InitializeClient();
    }

    public void InscriptionToServer()
    {
        if (InscriptionManager.Instance.PasswordField.text != InscriptionManager.Instance.RepeatPasswordField.text)
        {
            InscriptionManager.Instance.ErrorText.text = "Passwords doesn't match";
        }
        else
        {
            InscriptionManager.Instance.IsDoingRegister = true;
            this.player.Mail = InscriptionManager.Instance.EmailField.text;
            this.player.Pseudo = InscriptionManager.Instance.PseudoField.text;
            this.player.Password = InscriptionManager.Instance.PasswordField.text;
            InscriptionManager.Instance.Register.interactable = false;
            this.InitializeClient();
        }
    }

    private void InitializeClient()
    {
        this.client.PacketHandlers = new Dictionary<int, PacketHandler>()
            {
                { (int)ServerPackets.Welcome, this.sessionHandleBusiness.Welcome },
                { (int)ServerPackets.SendPlayerData, this.sessionHandleBusiness.DisplayPlayerData },
                { (int)ServerPackets.SendConnectionError, this.sessionHandleBusiness.SendConnectionError },
                { (int)ServerPackets.SendRegisterError, this.sessionHandleBusiness.SendRegisterError },
                { (int)ServerPackets.SendCreateGame, this.gameHandleBusiness.CreateGame },
                { (int)ServerPackets.SetJoinError, this.gameHandleBusiness.SetJoinError },
                { (int)ServerPackets.CanceledLoadingGame, this.gameHandleBusiness.CanceledLoadingGame },
                { (int)ServerPackets.SpawnMob, this.mobHandleBusiness.SpawnMob },
                { (int)ServerPackets.UpdateMobLife, this.mobHandleBusiness.UpdateMobLife },
                { (int)ServerPackets.KillMob, this.mobHandleBusiness.KillMob },
                { (int)ServerPackets.UpdateLife, this.gamerHandleBusiness.UpdateLife },
                { (int)ServerPackets.UpdateMoney, this.gamerHandleBusiness.UpdateMoney },
                { (int)ServerPackets.UpdateWaveNumber, this.mobHandleBusiness.UpdateWaveNumber },
                { (int)ServerPackets.Win, this.gameHandleBusiness.Win },
                { (int)ServerPackets.Lose, this.gameHandleBusiness.Lose },
                { (int)ServerPackets.Draw, this.gameHandleBusiness.Draw },
                { (int)ServerPackets.LoadGame, this.gameHandleBusiness.LoadGame },
                { (int)ServerPackets.SendLobbyCode, this.gameHandleBusiness.SetLobbyCode },
                { (int)ServerPackets.UpdateTowerPrice, this.towerHandleBusiness.UpdateTowerPrice },
                { (int)ServerPackets.InvokeTower, this.towerHandleBusiness.InvokeTower },
                { (int)ServerPackets.RemoveTower, this.towerHandleBusiness.RemoveTower },
                { (int)ServerPackets.MergeTower, this.towerHandleBusiness.MergeTower },
            };

        this.client.IsConnected = true;
        this.tcpCommunicationBusiness.Connect();
        this.client.Tcp.Socket.BeginConnect(this.client.Ip, this.client.Port, this.tcpCommunicationBusiness.ConnectCallback, this.client.Tcp.Socket);
    }
}