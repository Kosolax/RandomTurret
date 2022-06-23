using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Zenject;

public class InscriptionManager : MonoBehaviour
{
    public static InscriptionManager Instance;

    public InputField EmailField;

    public Text ErrorText;

    public bool IsDoingRegister;

    public InputField PasswordField;

    public InputField PseudoField;

    public Button Register;

    public InputField RepeatPasswordField;

    [Inject]
    private readonly ClientBusiness clientBusiness;

    public void Connection()
    {
        SceneManager.LoadScene("Connection", LoadSceneMode.Single);
    }

    public void RegisterAndConnectToServer()
    {
        this.clientBusiness.InscriptionToServer();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        this.IsDoingRegister = false;
    }
}