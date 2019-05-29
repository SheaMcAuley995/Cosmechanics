using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ASyncManager : MonoBehaviour
{
    public static ASyncManager instance;

    public AsyncOperation winOperation;
    public AsyncOperation loseOperation;

    [HideInInspector] public bool winning, losing;

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
        //LoadLoseScene();
        //LoadWinScene();
    }

    void LoadLoseScene()
    {
        losing = false;
        StartCoroutine(LoadLoseSceneAsync("LoseScene"));
    }

    void LoadWinScene()
    {
        winning = false;
        StartCoroutine(LoadWinSceneAsync("WinScene"));
    }

    IEnumerator LoadWinSceneAsync(string winScene)
    {
        yield return null;

        winOperation = SceneManager.LoadSceneAsync(winScene);
        winOperation.allowSceneActivation = false;

        if (winning)
        {
            winOperation.allowSceneActivation = true;
        }

        yield return null;
    }

    IEnumerator LoadLoseSceneAsync(string loseScene)
    {
        yield return null;

        loseOperation = SceneManager.LoadSceneAsync(loseScene);
        loseOperation.allowSceneActivation = false;

        if (losing)
        {
            loseOperation.allowSceneActivation = true;
        }

        yield return null;
    }
}
