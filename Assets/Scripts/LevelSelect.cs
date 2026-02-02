using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void Select1()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void Back()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
