using System;
using System.Collections.Generic;

using UnityEngine;

using Zenject;

public class GamerGameManager : MonoBehaviour
{
    public Gamer Gamer;

    public GamerGoldManager GamerGoldManager;

    public GamerHealthManager GamerHealthManager;

    public GameObject Map;

    public GameObject MobPrefab;

    public Transform StockBulletTransform;

    public Transform StockMobTransform;

    public GameObject TowerPrefab;

    public List<GameObject> TowerSlots;

    [Inject]
    private readonly TowerBusiness towerBusiness;

    public event Action<GameObject, GamerGameManager> OnDeath;

    public event Action<GameObject, GamerGameManager> OnDeathFromMovement;

    public int AutoIncrementId { get; set; }

    public int Id { get; set; }

    public List<GameObject> MobsInstantiated { get; set; }

    public Dictionary<int, TowerInGame> TowersInGame { get; set; }

    public void InvokeTower()
    {
        this.towerBusiness.InvokeTower();
    }

    private void Start()
    {
        this.TowersInGame = new Dictionary<int, TowerInGame>();
        this.AutoIncrementId = 1;
        this.MobsInstantiated = new List<GameObject>();
    }
}