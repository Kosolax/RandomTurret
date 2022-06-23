using UnityEngine;

using Zenject;

public class SessionManager : MonoBehaviour
{
    public static SessionManager Instance;

    [Inject]
    private readonly SessionBusiness sessionBusiness;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void OnApplicationQuit()
    {
        this.sessionBusiness.Disconnect();
    }
}