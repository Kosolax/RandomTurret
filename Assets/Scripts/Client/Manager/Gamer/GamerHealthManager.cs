using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class GamerHealthManager : MonoBehaviour
{
    public float CurrentLife;

    public Sprite EmptyHeart;

    public Sprite FullHeart;

    public List<Image> Hearts;

    public float MaxLife;

    public void RefreshHp(float currentLife)
    {
        this.CurrentLife = currentLife;
        if (this.CurrentLife > this.MaxLife)
        {
            this.CurrentLife = this.MaxLife;
        }

        float diff = this.MaxLife - this.CurrentLife;

        if (diff > 0)
        {
            for (int i = 0; i < diff; i++)
            {
                this.Hearts[(int)this.MaxLife - 1 - i].sprite = this.EmptyHeart;
            }
        }
    }

    private void Start()
    {
        this.Hearts = new List<Image>();
        for (int i = 0; i < this.MaxLife; i++)
        {
            GameObject gameObject = new GameObject();
            Image currentHeart = gameObject.AddComponent<Image>();
            gameObject.transform.SetParent(this.transform);
            currentHeart.sprite = this.FullHeart;
            this.Hearts.Add(currentHeart);
        }
    }
}