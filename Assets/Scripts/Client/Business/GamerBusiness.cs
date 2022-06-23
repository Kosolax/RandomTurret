public class GamerBusiness
{
    public void UpdateLife(int clientId, float life)
    {
        GamerGameManager gamerGameManager = GameManager.Instance.GamerGameManager.Id == clientId ? GameManager.Instance.GamerGameManager : GameManager.Instance.EnemyPlayerGameManager;
        if (gamerGameManager != null && gamerGameManager.GamerHealthManager != null)
        {
            gamerGameManager.GamerHealthManager.RefreshHp(life);
        }
    }

    public void UpdateMoney(int money)
    {
        GamerGameManager gamerGameManager = GameManager.Instance.GamerGameManager;
        if (gamerGameManager != null && gamerGameManager.GamerGoldManager)
        {
            gamerGameManager.GamerGoldManager.SetGold(money);
        }
    }
}