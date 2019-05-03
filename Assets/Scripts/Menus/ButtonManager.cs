using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    // BUILD INDEX KEY:
    // 0 = MainMenu_Updated
    // 1 = CharacterSelection
    // 2 = ZachOverWorld
    // 3 = BetaMichaelTest
    // 4 = Ship_Level_1Final

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
        players = FindObjectsOfType<SelectedPlayer>();

        SceneFader.instance.FadeTo(SceneManager.GetActiveScene().name.ToString());
        Time.timeScale = 1f;

        for (int i = 0; i < players.Length; i++)
        {
            players[i].transform.position = ExampleGameController.instance.spawnPoints[i];
        }
    }

    // Fades to character selection
    public void ContinueGame()
    {
        players = FindObjectsOfType<SelectedPlayer>();

        SceneFader.instance.FadeTo("ZachOverWorld");
        Time.timeScale = 1f;

        foreach (SelectedPlayer player in players)
        {
            player.transform.localScale = new Vector3(0f, 0f, 0f);
        }
    }
}
