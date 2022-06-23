using Assets.Scripts.Client.Resource;

using UnityEngine;
using UnityEngine.SceneManagement;

using Zenject;

public class SessionBusiness
{
    [Inject]
    private readonly Client client;

    [Inject]
    private readonly Player player;

    [Inject]
    private readonly SessionSendBusiness sessionSendBusiness;

    [Inject]
    private readonly TCPCommunicationBusiness tcpCommunicationBusiness;

    public void Disconnect()
    {
        if (this.client.IsConnected)
        {
            this.client.IsConnected = false;
            this.tcpCommunicationBusiness.Disconnect();
            this.client.Tcp.Stream = null;
            this.client.Tcp.ReceivedData = null;
            this.client.Tcp.ReceiveBuffer = null;
            this.client.Tcp.Socket = null;
            this.client.Id = default;
        }
    }

    public void DisplayConnectionError(string key)
    {
        string message = SessionResource.Get(key);
        ConnectionManager.Instance.ErrorText.text = string.Empty;
        ConnectionManager.Instance.ErrorText.text += $"{message}\n";
        ConnectionManager.Instance.ConnectButton.interactable = true;
    }

    public void DisplayPlayerData(Player player)
    {
        if (player != null)
        {
            this.player.SetPlayer(player);
            SceneManager.LoadScene("Main", LoadSceneMode.Single);
        }
        else
        {
            InscriptionManager.Instance.ErrorText.text = string.Empty;
            ConnectionManager.Instance.ErrorText.text = string.Empty;

            if (ConnectionManager.Instance != null)
            {
                ConnectionManager.Instance.ErrorText.text += SessionResource.Get("Password_Or_Email_Wrong");
                ConnectionManager.Instance.ConnectButton.interactable = true;
            }

            if (InscriptionManager.Instance != null)
            {
                InscriptionManager.Instance.ErrorText.text += SessionResource.Get("Password_Or_Email_Wrong");
                InscriptionManager.Instance.Register.interactable = true;
            }
        }
    }

    public void DisplayRegisterError(string key)
    {
        string message = SessionResource.Get(key);
        InscriptionManager.Instance.ErrorText.text = string.Empty;
        InscriptionManager.Instance.ErrorText.text += $"{message}\n";
        InscriptionManager.Instance.Register.interactable = true;
    }

    public void Welcome(int id, string message)
    {
        Debug.Log($"Message from server: {message}");
        this.client.Id = id;
        this.sessionSendBusiness.WelcomeReceived();
    }
}