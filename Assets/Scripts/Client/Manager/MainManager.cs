using UnityEngine;
using UnityEngine.UI;

using Zenject;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public Text Elo;

    public Text Gold;

    public GameObject LoadingModal;

    public GameObject LoadingPrivateModal;

    public Text LobbyCode;

    public Text LobbyJoinError;

    public Text Pseudo;

    [Inject]
    private readonly GameBusiness gameBusiness;

    [Inject]
    private readonly Player player;

    public void AskForGame()
    {
        this.gameBusiness.AskForGame(this.LoadingModal);
    }

    public void AskForPrivateGame()
    {
        this.gameBusiness.AskForPrivateGame(this.LoadingPrivateModal);
    }

    public void AskJoinPrivateGame()
    {
        string lobbyCodeAsString = this.LobbyCode.text;
        if (int.TryParse(lobbyCodeAsString, out int lobbyCode))
        {
            this.gameBusiness.AskJoinPrivateGame(lobbyCode);
        }
        else
        {
            this.LobbyJoinError.text = "You must provide a number";
        }
    }

    public void CancelAskForGame()
    {
        this.gameBusiness.CancelAskForGame();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        this.Pseudo.text = this.player.Pseudo;
        this.Gold.text = this.player.Gold.ToString();
        this.Elo.text = this.player.Elo.ToString();
    }
}