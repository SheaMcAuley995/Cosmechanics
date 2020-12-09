using System;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance = null;
    public Canvas pauseCanvas;
    bool pause;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        pause = false;
    }

    public void OnPauseUpdate(InputActionEventData data)
    {
        if (data.GetButtonDown())
        {
            PauseGame(pause);
        }
    }

    void PauseGame(bool set)
    {
        Time.timeScale = Convert.ToInt32(set);
        pauseCanvas.gameObject.SetActive(!set);
        pause = !set;
    }
}