using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class MobBusiness
{
    public void KillMob(int clientId, int indexMob)
    {
        GamerGameManager gamerGameManager = GameManager.Instance.GamerGameManager.Id == clientId ? GameManager.Instance.GamerGameManager : GameManager.Instance.EnemyPlayerGameManager;
        Mob mob = GameManager.Mobs[indexMob];
        HelperManager.Instance.DestroyGameObject(gamerGameManager.MobsInstantiated[indexMob]);
        gamerGameManager.MobsInstantiated.RemoveAt(indexMob);
        gamerGameManager.MobsInstantiated.RemoveAll(x => x == null);
    }

    public void SpawnMob(int mobId, int clientId, float difficultyMultiplier)
    {
        Mob mob = GameManager.Mobs.Where(x => x.Id == mobId).FirstOrDefault();
        GamerGameManager gamerGameManager = GameManager.Instance.GamerGameManager.Id == clientId ? GameManager.Instance.GamerGameManager : GameManager.Instance.EnemyPlayerGameManager;
        GameObject mobInScene = HelperManager.Instance.InstantiateGameObject(gamerGameManager.MobPrefab, gamerGameManager.StockMobTransform);
        MobInGame mobInGame = mobInScene.GetComponent<MobInGame>();
        Dictionary<StatType, float> stats = mob.MobStats.ToDictionary(x => x.Stat.StatType, x => x.Value);

        mobInGame.Initialize(gamerGameManager.AutoIncrementId, gamerGameManager.Map, mobInScene, stats, GameManager.Instance.MobsColor[(int) mob.MobType], difficultyMultiplier);

        gamerGameManager.MobsInstantiated.Add(mobInScene);
        gamerGameManager.AutoIncrementId++;
    }

    public void UpdateMobLife(int clientId, float currentLife, int idMob)
    {
        GamerGameManager gamerGameManager = GameManager.Instance.GamerGameManager.Id == clientId ? GameManager.Instance.GamerGameManager : GameManager.Instance.EnemyPlayerGameManager;
        if (gamerGameManager.MobsInstantiated.Count > 0)
        {
            MobInGame mobInGame = gamerGameManager.MobsInstantiated.Select(x => x.GetComponent<MobInGame>()).Where(x => x.Id == idMob).FirstOrDefault();
            if (mobInGame != null)
            {
                MobHealthManager mobHealthManager = mobInGame.MobHealthManager;
                mobHealthManager.UpdateLife(currentLife, mobInGame);
            }
        }
    }

    public void UpdateWaveNumber(int waveNumber)
    {
        GameManager.Instance.WaveNumberText.text = waveNumber.ToString();
    }
}