using System;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance = null;
    Canvas pauseCanvas;
    public Button button;
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
        pauseCanvas = GetComponent<Canvas>();
        pause = false;
    }

    public void OnPauseUpdate(InputActionEventData data)
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
        if (set == true)
        {
            button.Select();
        }
        pause = !set;
    }
}