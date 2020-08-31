using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ASyncSceneLoader : MonoBehaviour
{
    public static ASyncSceneLoader instance;

    [Header("Loading Screen Objects")] [Tooltip("Not required for functionality.")]
    public GameObject splashScreen;
    public Slider loadingBar;
    public TextMeshProUGUI loadingText;

    [Header("List of Scenes")] [Tooltip("Please fill these with the names of all game scenes.")]
    public List<string> scenes = new List<string>()
    {
        // TODO: PLACEHOLDER VALUES, REPLACE WHEN SCENE NAMES ARE SOLIDIFIED!
        "MainMenu", "LevelSelect", "Level1", "Level2", "Level3", "Level4",
        "Level5", "Level6", "Level7", "Level8", "Level9", "Level10"
    };

    bool loadingScene = false; // This is to control operation so that scenes are not loaded multiple times.

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

    // Call this for loading new scenes.
    public void StartASyncLoad(string sceneName)
    {
        loadingScene = true;

        ActivateSplashScreen();
        StartCoroutine(LoadNewScene(sceneName));
    }

    IEnumerator LoadNewScene(string scene)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(scene);
        
        while (!async.isDone)
        {
            float progress = Mathf.Clamp01(async.progress / 0.9f);

            if (loadingBar != null)
                loadingBar.value = progress;

            if (loadingText != null)
                loadingText.text = progress * 100f + "%";

            yield return null;
        }

        loadingScene = false;
    }

    // Activates the splash screen, loading bar, and text if they exist.
    void ActivateSplashScreen()
    {
        if (loadingBar != null)
            loadingBar.gameObject.SetActive(true);
        if (splashScreen != null)
            splashScreen.gameObject.SetActive(true);
        if (loadingText != null)
            loadingText.text = "Loading level...";
    }
}
