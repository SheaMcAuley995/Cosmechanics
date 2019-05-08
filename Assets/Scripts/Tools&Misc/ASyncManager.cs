using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ASyncManager : MonoBehaviour
{
    public static ASyncManager instance;
    public AsyncOperation winOperation;
    public AsyncOperation loseOperation;

    void Awake()
    {
        #region Singleton
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        #endregion
    }

    void Start()
    {
        LoadScenes();
    }

    public void LoadScenes()
    {
        StartCoroutine(LoadWinSceneAsync("WinScene"));
        StartCoroutine(LoadLoseSceneAsync("LoseScene"));
    }

    IEnumerator LoadWinSceneAsync(string scene)
    {
        winOperation = SceneManager.LoadSceneAsync(scene);
        winOperation.allowSceneActivation = false;

        while (!winOperation.isDone)
        {
            yield return null;
        }
    }

    IEnumerator LoadLoseSceneAsync(string scene)
    {
        loseOperation = SceneManager.LoadSceneAsync(scene);
        loseOperation.allowSceneActivation = false;

        while (!loseOperation.isDone)
        {
            yield return null;
        }
    }
}
