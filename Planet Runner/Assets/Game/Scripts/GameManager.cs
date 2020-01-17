using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        InitiateSingletonGameManager();
    }

    private void InitiateSingletonGameManager()
    {
        if (_instance && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void LoadScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    public void LoadNextScene()
    {
        int currentSceneID = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneID + 1);

        //StartCoroutine(GetLevelManager());
    }

    public void QuitGame()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }
}
