using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
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

    // Destroys any objects players are holding so that they aren't carried over through scenes w/ players.
    void DestroyPickups()
    {
        pickups = FindObjectsOfType<PickUp>();
        for (int i = 0; i < pickups.Length; i++)
        {
            Destroy(pickups[i].gameObject);
        }
    }

    // Fades to main menu
    public void ReturnToMenu()
    {
        players = FindObjectsOfType<SelectedPlayer>();

        DestroyPickups();

        SceneFader.instance.FadeTo("MainMenu_Update");
        GameStateManager.instance.SetGameState(GameState.Playing);

        // Destroys the active players so that new ones can be selected.
        foreach (SelectedPlayer player in players)
        {
            player.gameObject.AddComponent<CharToDestroy>();
            Destroy(player);
        }
    }

    // Fades to last level attempted
    public void RetryLevel()
    {
        DestroyPickups();

        SceneFader.instance.FadeTo(SceneManager.GetActiveScene().name);
        GameStateManager.instance.SetGameState(GameState.Playing);
    }

    // Fades to overworld
    public void ContinueGame()
    {
        players = FindObjectsOfType<SelectedPlayer>();

        DestroyPickups();

        SceneFader.instance.FadeTo("LevelSelectUpdated");

        // The only reason this still exists is to accomodate the horrible character spawning/transition system that we don't want to replace.
        foreach (SelectedPlayer player in players)
        {
            Animator[] animators = player.GetComponentsInChildren<Animator>();
            foreach (Animator animator in animators)
            {
                animator.Play("Idle", -1, 0);
            }

            player.transform.Rotate(0f, 0f, 0f);
            player.transform.position = new Vector3(0f, 500f, 0f);
        }
    }
}
