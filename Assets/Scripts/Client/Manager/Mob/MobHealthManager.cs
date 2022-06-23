using UnityEngine;
using UnityEngine.UI;

public class MobHealthManager : MonoBehaviour
{
    public Slider slider;

    public void Initialize(float maxLife)
    {
        this.SetMaxLife(maxLife);
    }

    public void UpdateLife(float life, MobInGame mobInGame)
    {
        mobInGame.Life = life;
        this.slider.value = life;
    }

    private void SetMaxLife(float maxLife)
    {
        this.slider.maxValue = maxLife;
        this.slider.value = maxLife;
    }
}