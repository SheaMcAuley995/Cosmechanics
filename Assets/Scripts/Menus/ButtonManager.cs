using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    // BUILD INDEX KEY:
    // 0 = MainMenu
    // 1 = CharacterSelection
    // 2 = ZachOverWorld
    // 3 = NewMichaelTest

    // Fades to character selection
    public void StartGame()
    {
        SceneFader.instance.FadeTo("CharacterSelection");
    }

    // Fades to quit
    public void QuitGame()
    {

        SceneFader.instance.FadeToQuit();
    }

    //Fades to main menu
    public void ReturnToMenu()
    {
        SceneFader.instance.FadeTo("MainMenu");
    }

    // Fades to current scene
    public void RetryLevel()
    {
        SceneFader.instance.FadeTo(SceneManager.GetActiveScene().name);
    }

    // Fades to character selection
    public void ContinueGame()
    {
        SceneFader.instance.FadeTo("CharacterSelection");
    }
}
