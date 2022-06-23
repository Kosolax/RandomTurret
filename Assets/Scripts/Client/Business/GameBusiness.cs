using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.SceneManagement;

using Zenject;

public class GameBusiness
{
    [Inject]
    private readonly GameSendBusiness gameSendBusiness;

    [Inject]
    private readonly Player player;

    public void AskForGame(GameObject loadingModal)
    {
        loadingModal.SetActive(true);
        this.gameSendBusiness.AskForGame();
    }

    public void AskForPrivateGame(GameObject loadingPrivateModal)
    {
        loadingPrivateModal.SetActive(true);
        this.gameSendBusiness.AskForPrivateGame();
    }

    public void AskJoinPrivateGame(int lobbyCode)
    {
        this.gameSendBusiness.AskJoinPrivateGame(lobbyCode);
    }

    public void CancelAskForGame()
    {
        this.gameSendBusiness.CancelAskForGame();
    }

    public void CanceledLoadingGame()
    {
        MainManager.Instance.LoadingModal.SetActive(false);
        MainManager.Instance.LoadingPrivateModal.SetActive(false);
    }

    public void CreateGame()
    {
        GameManager.Instance.WaitingForOpponentPopIn.SetActive(false);
    }

    public void Draw(int newElo)
    {
        int eloDiff = newElo - GameManager.YourElo;
        this.player.Elo = newElo;
        string sign = eloDiff > 0 ? "You gained +" : "You lost ";
        GameManager.Instance.DrawPopIn.GetComponentInChildren<PopInManager>().Content = $"{sign}{eloDiff} elo";
        GameManager.Instance.DrawPopIn.SetActive(true);
    }

    public void LoadGame(string enemyName, List<Tower> enemyTowers, List<Tower> myTowers, int enemyElo, int yourElo, List<Mob> mobs)
    {
        GameManager.Mobs = mobs;
        GameManager.EnemyName = enemyName;
        GameManager.EnemyTowers = enemyTowers;
        this.player.Towers = myTowers;
        this.gameSendBusiness.LoadGameDone();
        GameManager.YourElo = yourElo;
        GameManager.EnemyElo = enemyElo;
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void Lose(int newElo)
    {
        int eloDiff = newElo - GameManager.YourElo;
        this.player.Elo = newElo;
        GameManager.Instance.LosePopIn.GetComponentInChildren<PopInManager>().Content = $"You lost {eloDiff} elo";
        GameManager.Instance.LosePopIn.SetActive(true);
    }

    public void SetLobbyCode(int lobbyCode)
    {
        MainManager.Instance.LoadingPrivateModal.GetComponentInChildren<PopInManager>().ContentText.text = $"You can invite your friends by giving them this code : {lobbyCode}";
        MainManager.Instance.LoadingPrivateModal.SetActive(true);
    }

    public void Win(int newElo)
    {
        int eloDiff = newElo - GameManager.YourElo;
        this.player.Elo = newElo;
        GameManager.Instance.WinPopIn.GetComponentInChildren<PopInManager>().Content = $"You gained +{eloDiff} elo";
        GameManager.Instance.WinPopIn.SetActive(true);
    }
}