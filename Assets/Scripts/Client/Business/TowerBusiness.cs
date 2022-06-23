using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Zenject;

public class TowerBusiness
{
    [Inject]
    private readonly TowerSendBusiness towerSendBusiness;

    public void InvokeTower(int clientId, int towerSlotIndex, int towerIndex, int level)
    {
        GamerGameManager gamerGameManager = GameManager.Instance.GamerGameManager.Id == clientId ? GameManager.Instance.GamerGameManager : GameManager.Instance.EnemyPlayerGameManager;
        DragAndDropTowerManager dragAndDropTowerManager = gamerGameManager.TowerSlots[towerSlotIndex].gameObject.GetComponent<DragAndDropTowerManager>();
        GameObject towerInScene = HelperManager.Instance.InstantiateGameObject(gamerGameManager.TowerPrefab, dragAndDropTowerManager.gameObject.transform);
        TowerInGame towerInGame = towerInScene.GetComponent<TowerInGame>();
        towerInGame.Image.color = GameManager.Instance.TowersColor[(int) gamerGameManager.Gamer.Towers[towerIndex].TowerType];
        // IAim is used to select the mob we want to shoot on
        IAim shootType = this.GetIAimFromShootType(gamerGameManager.Gamer.Towers[towerIndex].ShootType);
        IMerge mergeType = this.GetIMergeFromMergeType(gamerGameManager.Gamer.Towers[towerIndex].MergeType);
        Dictionary<StatType, float> stats = gamerGameManager.Gamer.Towers[towerIndex].TowerStats.ToDictionary(x => x.Stat.StatType, x => x.Value);
        towerInGame.Initialize(GameManager.Instance.TowersColor[(int) gamerGameManager.Gamer.Towers[towerIndex].TowerType], level, gamerGameManager, this, gamerGameManager.StockBulletTransform, shootType, mergeType, stats);
        dragAndDropTowerManager.TowerInGame = towerInGame;
        dragAndDropTowerManager.Index = towerSlotIndex;

        gamerGameManager.TowersInGame.Add(towerSlotIndex, towerInGame);
    }

    public void InvokeTower()
    {
        this.towerSendBusiness.InvokeTower();
    }

    public void MergeTower(int draggedTowerIndex, int droppedTowerIndex)
    {
        this.towerSendBusiness.MergeTower(draggedTowerIndex, droppedTowerIndex);
    }

    public void MergeTowerFromServer(int clientId, int draggedTowerIndex, int droppedTowerIndex)
    {
        GamerGameManager gamerGameManager = GameManager.Instance.GamerGameManager.Id == clientId ? GameManager.Instance.GamerGameManager : GameManager.Instance.EnemyPlayerGameManager;
        IMerge savedMergeType = gamerGameManager.TowerSlots[droppedTowerIndex].GetComponentInChildren<TowerInGame>().MergeType;

        this.RemoveTower(clientId, draggedTowerIndex);
        this.RemoveTower(clientId, droppedTowerIndex);

        savedMergeType.AnimationAndChangeStats(gamerGameManager);
    }

    public void RemoveTower(int clientId, int towerIndex)
    {
        GamerGameManager gamerGameManager = GameManager.Instance.GamerGameManager.Id == clientId ? GameManager.Instance.GamerGameManager : GameManager.Instance.EnemyPlayerGameManager;

        gamerGameManager.TowersInGame.Remove(towerIndex);

        gamerGameManager.TowerSlots[towerIndex].GetComponentInChildren<TowerInGame>().DestroyTower();
    }

    public void Shoot(List<GameObject> mobsSelected, GameObject bulletPrefab, Transform stockBulletTransform, GameObject gameObject, TowerInGame towerInGame, GamerGameManager gamerGameManager)
    {
        if (gamerGameManager.MobsInstantiated.Count > 0 && gamerGameManager.MobsInstantiated[0] != null)
        {
            foreach (GameObject mobSelected in mobsSelected)
            {
                GameObject bullet = HelperManager.Instance.InstantiateGameObject(bulletPrefab, stockBulletTransform);
                bullet.transform.position = gameObject.transform.position;

                BulletManager bulletManager = bullet.GetComponent<BulletManager>();
                bulletManager.TowerInGame = towerInGame;
                bulletManager.MobInGame = mobSelected.GetComponent<MobInGame>();
            }
        }
    }

    public void UpdateTowerPrice(int towerPrice)
    {
        GameManager.Instance.TowerPriceText.text = towerPrice.ToString();
    }

    /// <summary>
    /// Return the good script depending on the ShootType
    /// </summary>
    /// <param name="shootType"></param>
    /// <returns></returns>
    private IAim GetIAimFromShootType(ShootType shootType)
    {
        // set shoot type to first because api is not using shoottype right now
        shootType = ShootType.First;
        switch (shootType)
        {
            case ShootType.BiggestHp:
                return new BiggestHpShootType();
            case ShootType.First:
                return new FirstShootType();
        }

        return null;
    }

    /// <summary>
    /// Return the good script depending on the MergetType
    /// </summary>
    /// <param name="mergeType"></param>
    /// <returns></returns>
    private IMerge GetIMergeFromMergeType(MergeType MergeType)
    {
        // set merge type to SlowMobs because api is not using mergetype right now
        MergeType = MergeType.SlowMobs;
        switch (MergeType)
        {
            case MergeType.SlowMobs:
                return new SlowMobsMergeType();
            case MergeType.AttackSpeedForAllTowers:
                return new AttackSpeedForAllTowersMergeType();
        }

        return null;
    }
}