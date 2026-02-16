using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;
using Unity.Netcode.Transports.UTP;

public class NetworkMangerUI : MonoBehaviour
{
    [SerializeField] private Button serverBtn;
    [SerializeField] private Button hostBtn;
    [SerializeField] TMP_InputField codeInput;
    [SerializeField] private Button clientBtn;
    [SerializeField] TextMeshProUGUI statusText;

    //private void Awake()
    //{
    //    serverBtn.onClick.AddListener(() =>
    //    {
    //        NetworkManager.Singleton.StartServer();
    //    });
    //    hostBtn.onClick.AddListener(() =>
    //    {
    //        NetworkManager.Singleton.StartHost();
    //        Debug.Log("host startet");
    //    });
    //    clientBtn.onClick.AddListener(() =>
    //    {
    //        NetworkManager.Singleton.StartClient();
    //    });
    //}


    private void Awake()
    {
        hostBtn.onClick.AddListener(StartHost);
        clientBtn.onClick.AddListener(StartClient);
    }

    private void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += OnConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnDisconnected;
    }

    void StartHost()
    {
        string code = GenerateCode();
        LobbyData.HostCode = code;

        NetworkManager.Singleton.StartHost();
        if (statusText != null)
        {
            statusText.text = "Host Code: " + code;
        }
        Debug.Log(code);
    }

    void StartClient()
    {
        string enteredCode = codeInput.text;

        if (enteredCode != LobbyData.HostCode)
        {
            if (statusText != null)
            {
                statusText.text = "Wrong Code!";
            }
            return;
        }

        statusText.text = "Code OK - Connecting...";
        NetworkManager.Singleton.StartClient();
    }

    void OnConnected(ulong clientId)
    {
        if (clientId == NetworkManager.Singleton.LocalClientId)
        {
            if (statusText != null)
            {

                statusText.text = "Connected!";
            }
        }
    }

    void OnDisconnected(ulong clientId)
    {
        if (clientId == NetworkManager.Singleton.LocalClientId)
        {
            if (statusText != null)
            {
                statusText.text = "Connection Failed";

            }
        }
    }

    

    string GenerateCode()
    {
        return Random.Range(1000, 9999).ToString();
    }

}
