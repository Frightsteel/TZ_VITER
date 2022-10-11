using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(gameObject.scene.buildIndex);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(gameObject.scene.buildIndex + 1);
    }
}
