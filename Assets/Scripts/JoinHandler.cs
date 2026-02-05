using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

public class NetworkUI : MonoBehaviour
{
    [SerializeField] TMP_InputField codeInput;
    [SerializeField] TextMeshProUGUI statusText;

    void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += OnConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnDisconnected;
    }

    public void JoinGame()
    {
        string code = codeInput.text;

        var transport =
            NetworkManager.Singleton.GetComponent<UnityTransport>();

        transport.ConnectionData.Address = code;
        transport.ConnectionData.Port = 7777;

        statusText.text = "Connecting...";
        NetworkManager.Singleton.StartClient();
    }

    void OnConnected(ulong clientId)
    {
        if (clientId == NetworkManager.Singleton.LocalClientId)
        {
            statusText.text = "Connected!";
        }
    }

    void OnDisconnected(ulong clientId)
    {
        if (clientId == NetworkManager.Singleton.LocalClientId)
        {
            statusText.text = "Wrong code or host offline";
        }
    }
}

