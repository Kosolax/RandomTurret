using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Zenject;
public class ConnectionManager : MonoBehaviour
{
    public static ConnectionManager Instance;

    public Button ConnectButton;

    public Text ErrorText;

    public bool IsDoingConnection;

    public InputField MailField;

    public InputField PasswordField;

    [Inject]
    private readonly ClientBusiness clientBusiness;

    public void ConnectToServer()
    {
        this.clientBusiness.ConnectionToServer();
    }

    public void Register()
    {
        SceneManager.LoadScene("Inscription", LoadSceneMode.Single);
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

        this.IsDoingConnection = false;
    }
}