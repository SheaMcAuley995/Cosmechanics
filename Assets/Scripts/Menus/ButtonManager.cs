using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    // BUILD INDEX KEY:
    // 0 = MainMenu_Updated
    // 1 = CharacterSelection_Update
    // 2 = ZachOverWorld
    // 3 = BetaMichaelTest
    // 4 = Ship_Level_1Final
    // 5 = LoseScene
    // 6 = WinScene

    SelectedPlayer[] players;
    PickUp[] pickups;


    // Fades to character selection
    public void StartGame()
    {
        SceneFader.instance.FadeTo("CharacterSelection_Update");
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
        pickups = FindObjectsOfType<PickUp>();

        for (int i = 0; i < pickups.Length; i++)
        {
            Destroy(pickups[i].gameObject);
        }

        SceneFader.instance.FadeTo("MainMenu_Update");
        Time.timeScale = 1f;

        foreach (SelectedPlayer player in players)
        {
            player.gameObject.AddComponent<CharToDestroy>();
            Destroy(player);
        }
    }

    // Fades to last level attempted
    public void RetryLevel()
    {
        players = FindObjectsOfType<SelectedPlayer>();
        pickups = FindObjectsOfType<PickUp>();

        for (int i = 0; i < pickups.Length; i++)
        {
            Destroy(pickups[i].gameObject);
        }

        SceneFader.instance.FadeTo(players[0].currentScene);
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
        pickups = FindObjectsOfType<PickUp>();

        for (int i = 0; i < pickups.Length; i++)
        {
            Destroy(pickups[i].gameObject);
        }

        SceneFader.instance.FadeTo("ZachOverWorld");
        Time.timeScale = 1f;

        foreach (SelectedPlayer player in players)
        {
            player.transform.localScale = new Vector3(0f, 0f, 0f);
        }
    }
}
