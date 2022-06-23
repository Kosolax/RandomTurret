using System.Collections.Generic;

using UnityEngine;

public class Gamer : MonoBehaviour
{
    public Gamer()
    {
    }

    public Gamer(int id)
    {
        this.TowerPrice = 10;
        this.Id = id;
        this.Money = 10000;
        this.Life = 3;
    }

    public int Id { get; set; }

    public float Life { get; set; }

    public int Money { get; set; }

    public string Name { get; set; }

    public int TowerPrice { get; set; }

    public List<Tower> Towers { get; set; }
}