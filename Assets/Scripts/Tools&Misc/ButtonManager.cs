using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void StartGame()
    {

    }

    public void ResumeGame()
    {
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnToMenu(string scene)
    {
        // TODO: Scene fader
        SceneManager.LoadScene(scene);
    }

    public void RetryLevel(string thisScene)
    {
        // TODO: Scene fader 
        // (also this might be tricker than I'm thinking cause we'll need to store character data?)
        SceneManager.LoadScene(thisScene);
    }
}
