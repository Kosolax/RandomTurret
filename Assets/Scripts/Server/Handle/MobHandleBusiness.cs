using System.Threading.Tasks;

using Zenject;

public class MobHandleBusiness
{
    [Inject]
    private readonly MobBusiness mobBusiness;

    public Task KillMob(Packet packet)
    {
        int clientId = packet.ReadInt();
        int indexMob = packet.ReadInt();

        this.mobBusiness.KillMob(clientId, indexMob);
        return Task.CompletedTask;
    }

    public Task SpawnMob(Packet packet)
    {
        int mobId = packet.ReadInt();
        int clientId = packet.ReadInt();
        float difficultyMultiplier = packet.ReadFloat();

        this.mobBusiness.SpawnMob(mobId, clientId, difficultyMultiplier);
        return Task.CompletedTask;
    }

    public Task UpdateMobLife(Packet packet)
    {
        int clientId = packet.ReadInt();
        float currentLife = packet.ReadFloat();
        int idMob = packet.ReadInt();

        this.mobBusiness.UpdateMobLife(clientId, currentLife, idMob);
        return Task.CompletedTask;
    }

    public Task UpdateWaveNumber(Packet packet)
    {
        int waveNumber = packet.ReadInt();

        this.mobBusiness.UpdateWaveNumber(waveNumber);
        return Task.CompletedTask;
    }
}