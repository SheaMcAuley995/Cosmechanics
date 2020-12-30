using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    PlayerController[] controllers;
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
        SceneFader.instance.FadeTo("CharacterSelection_Update");
    }

    // Fades to quit
    public void QuitGame()
    {
        SceneFader.instance.FadeToQuit();
    }

    // Drops and destroys any objects currently held by players so they can pick up more objects in other levels.
    void HandlePickups()
    {
        controllers = FindObjectsOfType<PlayerController>();
        for (int i = 0; i < controllers.Length; i++)
        {
            if (controllers[i].interactedObject)
            {
                if (controllers[i].interactedObject.GetComponent<PickUp>())
                {
                    controllers[i].interactedObject.GetComponent<PickUp>().putMeDown(1.0f);
                }
            }
            controllers[i].interactedObject = null;
            controllers[i].animator.SetBool("isCarrying", false);
            controllers[i].blockMovement = false;
            controllers[i].SetpickedUp(false);
        }

        pickups = FindObjectsOfType<PickUp>();
        for (int i = 0; i < pickups.Length; i++)
        {
            Destroy(pickups[i].gameObject);
        }
    }

    // Fades to main menu. Pickups don't need to be handled because main menu destroys them & old characters before continuing to character select.
    public void ReturnToMenu()
    {
        SceneFader.instance.FadeTo("MainMenu_Update");
        GameStateManager.instance.SetGameState(GameState.Playing);
    }

    // Fades to level select.
    public void RetryLevel()
    {
        HandlePickups();
        //SceneFader.instance.FadeTo(SceneManager.GetActiveScene().name);
        SceneFader.instance.FadeTo("LevelSelectUpdated");
        GameStateManager.instance.SetGameState(GameState.Playing);
    }

    // Fades to level select.
    public void ContinueGame()
    {
        HandlePickups();
        SceneFader.instance.FadeTo("LevelSelectUpdated");
        GameStateManager.instance.SetGameState(GameState.Playing);
    }
}
