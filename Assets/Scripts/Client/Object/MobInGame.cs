using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class MobInGame : MonoBehaviour
{
    public Image Image;

    public MobEffectManager MobEffectManager;

    public MobHealthManager MobHealthManager;

    public MobMoveManager MobMoveManager;

    public int Id { get; set; }

    public float Life { get; set; }

    public GameObject Map { get; set; }

    public GameObject MobInScene { get; set; }

    public Dictionary<StatType, float> Stats { get; set; }

    public void Initialize(int id, GameObject map, GameObject mobInScene, Dictionary<StatType, float> stats, Color color, float difficultyMultiplier)
    {
        this.Id = id;
        this.Map = map;
        this.MobInScene = mobInScene;
        this.Stats = stats;
        this.Image.color = color;
        this.Life = this.Stats[StatType.MaxHealth] * difficultyMultiplier;
        this.MobMoveManager.Initialize(this.Map, this.MobInScene, this.Stats[StatType.Speed]);
        this.MobHealthManager.Initialize(this.Stats[StatType.MaxHealth] * difficultyMultiplier);
        this.MobEffectManager.Initialize(this);
    }

    private void ClearEvents()
    {
        this.MobMoveManager.OnDeath -= this.WantToDestroyFromMovement;
    }

    private void Start()
    {
        this.MobMoveManager.OnDeath += this.WantToDestroyFromMovement;
    }

    private void WantToDestroyFromMovement()
    {
        this.ClearEvents();
        Destroy(this.gameObject);
    }
}