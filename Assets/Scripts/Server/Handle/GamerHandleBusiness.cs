using System.Threading.Tasks;

using Zenject;

public class GamerHandleBusiness
{
    [Inject]
    private readonly GamerBusiness gamerBusiness;

    public Task UpdateLife(Packet packet)
    {
        int clientId = packet.ReadInt();
        float life = packet.ReadFloat();

        this.gamerBusiness.UpdateLife(clientId, life);
        return Task.CompletedTask;
    }

    public Task UpdateMoney(Packet packet)
    {
        int money = packet.ReadInt();

        this.gamerBusiness.UpdateMoney(money);
        return Task.CompletedTask;
    }
}