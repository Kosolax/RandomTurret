public class SlowMobsMergeType : IMerge
{
    public void AnimationAndChangeStats(GamerGameManager gamerGameManager)
    {
        int count = gamerGameManager.StockMobTransform.childCount;
        for (int i = 0; i < count; i++)
        {
            //change stats for all the current mob in the scene
            MobInGame mobInGame = gamerGameManager.StockMobTransform.GetChild(i).GetComponent<MobInGame>();
            IEffect effect = new SlowMob(5.0f, (float) 50 / 100, mobInGame);
            mobInGame.MobEffectManager.Effects.Add(effect);
            effect.ApplyEffect();
        }
    }
}