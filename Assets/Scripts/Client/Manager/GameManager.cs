using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Zenject;

public class GameManager : MonoBehaviour
{
    public static int EnemyElo;

    public static string EnemyName;

    public static List<Tower> EnemyTowers;

    public static GameManager Instance;

    public static List<Mob> Mobs;

    public static int YourElo;

    public GameObject DrawPopIn;

    public GamerGameManager EnemyPlayerGameManager;

    public Text EnemyTextElo;

    public Text EnemyTextName;

    public GamerGameManager GamerGameManager;

    public GameObject LosePopIn;

    public List<Color> MobsColor;

    public Text TowerPriceText;

    public List<Color> TowersColor;

    public GameObject WaitingForOpponentPopIn;

    public Text WaveNumberText;

    public GameObject WinPopIn;

    public Text YourTextElo;

    public Text YourTextName;

    [Inject]
    private readonly Client client;

    [Inject]
    private readonly Player player;

    public void GoToMenu()
    {
        this.DrawPopIn.GetComponentInChildren<PopInManager>().ContentText.text = this.DrawPopIn.GetComponentInChildren<PopInManager>().Content;
        this.WinPopIn.GetComponentInChildren<PopInManager>().ContentText.text = this.WinPopIn.GetComponentInChildren<PopInManager>().Content;
        this.LosePopIn.GetComponentInChildren<PopInManager>().ContentText.text = this.LosePopIn.GetComponentInChildren<PopInManager>().Content;
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            this.GamerGameManager.Id = this.client.Id;
            this.GamerGameManager.Gamer.Towers = this.player.Towers;
            this.EnemyPlayerGameManager.Gamer.Towers = EnemyTowers;
            this.EnemyTextName.text = EnemyName;
            this.YourTextName.text = this.player.Pseudo;
            this.YourTextElo.text = YourElo.ToString();
            this.EnemyTextElo.text = EnemyElo.ToString();
        }
        else
        {
            Destroy(this);
        }
    }
}