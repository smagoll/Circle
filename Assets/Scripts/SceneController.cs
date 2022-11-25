using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1;
    }
}
