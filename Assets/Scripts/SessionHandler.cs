using Unity.Netcode;
using UnityEngine;

public class SessionHandler : MonoBehaviour
{
    public void StopSession()
    {
        NetworkManager.Singleton.Shutdown();
    }
}
