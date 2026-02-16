using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    string selectedScene = "Map1"; // default

    public void SelectMap(string sceneName)
    {
        selectedScene = sceneName;
        Debug.Log("selected map: " + sceneName);
        StartHost();
    }

    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();

        NetworkManager.Singleton.SceneManager.LoadScene(selectedScene, LoadSceneMode.Single);
    }

    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
    }
}
