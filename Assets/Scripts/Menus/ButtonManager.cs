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

    SelectedPlayer[] players;

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
        players = FindObjectsOfType<SelectedPlayer>();

        SceneFader.instance.FadeTo("MainMenu_Update");
        Time.timeScale = 1f;

        foreach (SelectedPlayer player in players)
        {
            player.gameObject.AddComponent<CharToDestroy>();
            Destroy(player);
        }
    }

    // Fades to current scene
    public void RetryLevel()
    {
        SceneFader.instance.FadeTo(SceneManager.GetActiveScene().name.ToString());
        Time.timeScale = 1f;
    }

    // Fades to character selection
    public void ContinueGame()
    {
        SceneFader.instance.FadeTo("CharacterSelection");
    }
}
