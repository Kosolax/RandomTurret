using System.Threading.Tasks;

using Newtonsoft.Json;

using Zenject;

public class SessionHandleBusiness
{
    [Inject]
    private readonly SessionBusiness sessionBusiness;

    public Task DisplayPlayerData(Packet packet)
    {
        Player player = JsonConvert.DeserializeObject<Player>(packet.ReadString());
        this.sessionBusiness.DisplayPlayerData(player);

        return Task.CompletedTask;
    }

    public Task SendConnectionError(Packet packet)
    {
        string message = packet.ReadString();
        this.sessionBusiness.DisplayConnectionError(message);

        return Task.CompletedTask;
    }

    public Task SendRegisterError(Packet packet)
    {
        string message = packet.ReadString();
        this.sessionBusiness.DisplayRegisterError(message);

        return Task.CompletedTask;
    }

    public Task Welcome(Packet packet)
    {
        string msg = packet.ReadString();
        int id = packet.ReadInt();
        this.sessionBusiness.Welcome(id, msg);

        return Task.CompletedTask;
    }
}