using System.Collections.Generic;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Zenject;

public class GameHandleBusiness
{
    [Inject]
    private readonly GameBusiness gameBusiness;

    public Task CanceledLoadingGame(Packet packet)
    {
        this.gameBusiness.CanceledLoadingGame();
        return Task.CompletedTask;
    }

    public Task CreateGame(Packet packet)
    {
        this.gameBusiness.CreateGame();
        return Task.CompletedTask;
    }

    public Task Draw(Packet packet)
    {
        int newElo = packet.ReadInt();
        this.gameBusiness.Draw(newElo);
        return Task.CompletedTask;
    }

    public Task LoadGame(Packet packet)
    {
        string enemyName = packet.ReadString();
        List<Tower> enemyTowers = JsonConvert.DeserializeObject<List<Tower>>(packet.ReadString());
        List<Tower> myTowers = JsonConvert.DeserializeObject<List<Tower>>(packet.ReadString());
        int enemyElo = packet.ReadInt();
        int yourElo = packet.ReadInt();
        List<Mob> mobs = JsonConvert.DeserializeObject<List<Mob>>(packet.ReadString());
        this.gameBusiness.LoadGame(enemyName, enemyTowers, myTowers, enemyElo, yourElo, mobs);
        return Task.CompletedTask;
    }

    public Task Lose(Packet packet)
    {
        int newElo = packet.ReadInt();
        this.gameBusiness.Lose(newElo);
        return Task.CompletedTask;
    }

    public Task SetJoinError(Packet packet)
    {
        MainManager.Instance.LobbyJoinError.text = "The game doesn't exist";
        return Task.CompletedTask;
    }

    public Task SetLobbyCode(Packet packet)
    {
        int lobbyCode = packet.ReadInt();
        this.gameBusiness.SetLobbyCode(lobbyCode);
        return Task.CompletedTask;
    }

    public Task Win(Packet packet)
    {
        int newElo = packet.ReadInt();
        this.gameBusiness.Win(newElo);
        return Task.CompletedTask;
    }
}