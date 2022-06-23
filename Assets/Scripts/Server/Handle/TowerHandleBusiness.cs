using System.Threading.Tasks;

using Zenject;

public class TowerHandleBusiness
{
    [Inject]
    private readonly TowerBusiness towerBusiness;

    public Task InvokeTower(Packet packet)
    {
        int clientId = packet.ReadInt();
        int pathMapIndex = packet.ReadInt();
        int towerIndex = packet.ReadInt();
        int level = packet.ReadInt();

        this.towerBusiness.InvokeTower(clientId, pathMapIndex, towerIndex, level);
        return Task.CompletedTask;
    }

    public Task MergeTower(Packet packet)
    {
        int clientId = packet.ReadInt();
        int draggedTowerIndex = packet.ReadInt();
        int droppedTowerIndex = packet.ReadInt();

        this.towerBusiness.MergeTowerFromServer(clientId, draggedTowerIndex, droppedTowerIndex);
        return Task.CompletedTask;
    }

    public Task RemoveTower(Packet packet)
    {
        int clientId = packet.ReadInt();
        int draggedTowerIndex = packet.ReadInt();

        this.towerBusiness.RemoveTower(clientId, draggedTowerIndex);
        return Task.CompletedTask;
    }

    public Task UpdateTowerPrice(Packet packet)
    {
        int towerPrice = packet.ReadInt();

        this.towerBusiness.UpdateTowerPrice(towerPrice);
        return Task.CompletedTask;
    }
}