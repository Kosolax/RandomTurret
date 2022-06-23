using UnityEngine;
using UnityEngine.UI;

public class PopInManager : MonoBehaviour
{
    public Text ButtonText;

    public string Content;

    public Text ContentText;

    public string NewButtonText;

    public string Title;

    public Text TitleText;

    private void Start()
    {
        this.TitleText.text = this.Title;
        this.ButtonText.text = this.NewButtonText;
        this.ContentText.text = this.Content;
    }
}