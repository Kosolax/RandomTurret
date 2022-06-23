using UnityEngine;
using UnityEngine.UI;

public class GamerGoldManager : MonoBehaviour
{
    public Text GoldText;

    public int GoldValue;

    public void SetGold(int amount)
    {
        this.GoldValue = amount;
        this.RefreshGoldScore();
    }

    public void Start()
    {
        this.GoldValue = 10000;
        this.RefreshGoldScore();
    }

    private void RefreshGoldScore()
    {
        this.GoldText.text = this.GoldValue.ToString();
    }
}