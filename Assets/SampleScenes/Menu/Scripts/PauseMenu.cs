using System;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class PauseMenu : MonoBehaviour
{
    public Canvas pauseCanvas;
    int playerID = 0;
    Player player;
    bool pause;
    private void Start()
    {
        pause = false;

        player = ReInput.players.GetPlayer(playerID);

        player.AddInputEventDelegate(OnPauseUpdate, UpdateLoopType.Update, "Pause");
    }

    void OnPauseUpdate(InputActionEventData data)
    {
        if (data.GetButtonDown())
        {
            PauseGame(pause);
        }
    }

    public void PauseGame(bool set)
    {
        Time.timeScale = Convert.ToInt32(set);
        pauseCanvas.gameObject.SetActive(!set);
        pause = !set;
    }
}