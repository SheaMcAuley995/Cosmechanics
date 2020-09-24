using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    // BUILD INDEX KEY:
    // 0 = MainMenu_Updated
    // 1 = CharacterSelection_Update
    // 2 = CacieOverWorld
    // 3 = BetaMichaelTest
    // 4 = Ship_Level_1Final
    // 5 = LoseScene
    // 6 = WinScene

    PlayerController[] controllers;
    SelectedPlayer[] players;
    CharToDestroy[] oldPlayers;
    PickUp[] pickups;


    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);
        controllers = FindObjectsOfType<PlayerController>();
        foreach (PlayerController controller in controllers)
        {
            controller.cameraTrans = Camera.main.transform;
        }
    }

    // Fades to character selection
    public void StartGame()
    {
        oldPlayers = FindObjectsOfType<CharToDestroy>();
        SceneFader.instance.FadeTo("CharacterSelection_Update");
        foreach (CharToDestroy player in oldPlayers)
        {
            Destroy(player.gameObject);
        }
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

        Time.timeScale = 1f;
        SceneFader.instance.FadeTo("MainMenu_Update");
        GameStateManager.instance.SetGameState(GameState.Playing);

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
        GameStateManager.instance.SetGameState(GameState.Playing);
        Time.timeScale = 1f;

        for (int i = 0; i < players.Length; i++)
        {
            players[i].transform.position = CharacterHandler.instance.spawnPoints[i];
        }
    }

    // Fades to overworld
    public void ContinueGame()
    {
        players = FindObjectsOfType<SelectedPlayer>();
        pickups = FindObjectsOfType<PickUp>();

        for (int i = 0; i < pickups.Length; i++)
        {
            Destroy(pickups[i].gameObject);
        }

        SceneFader.instance.FadeTo("CacieOverworld");
        Time.timeScale = 1f;

        foreach (SelectedPlayer player in players)
        {
            Animator[] animators = player.GetComponentsInChildren<Animator>();
            foreach (Animator animator in animators)
            {
                animator.Play("Idle", -1, 0);
            }

            player.transform.Rotate(0f, 0f, 0f);
            player.transform.localScale = new Vector3(0f, 0f, 0f);
        }
    }
}
