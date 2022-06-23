public class AttackSpeedForAllTowersMergeType : IMerge
{
    public void AnimationAndChangeStats(GamerGameManager gamerGameManager)
    {
        foreach (int towerIndexInDict in gamerGameManager.TowersInGame.Keys)
        {
            TowerInGame towerInGame = gamerGameManager.TowersInGame[towerIndexInDict];
            IEffect effect = new AttackSpeedTower(5.0f, (float) 50 / 100, towerInGame);
            towerInGame.TowerEffectManager.Effects.Add(effect);
            effect.ApplyEffect();
        }
    }
}