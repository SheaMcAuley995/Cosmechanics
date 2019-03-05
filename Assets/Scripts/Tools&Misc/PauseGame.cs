using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    [Header("UI Elements")]
    public Canvas pauseCanvas;
    public Image pausePanel;

    [Header("Configurations")]
    public KeyCode pauseButton1;
    public KeyCode pauseButton2;

    Color menuFaded = new Color(0f, 0.5108867f, 1f, 0f);
    Color menuTransparent = new Color(0f, 0.5108867f, 1f, 0.5882353f);
    bool input;

    #region KeyCode caching
    KeyCode defaultPauseButton1 = KeyCode.Escape;
    KeyCode defaultPauseButton2 = KeyCode.P;
    KeyCode none = KeyCode.None;
    #endregion

    // Use this for initialization
    void Start ()
    {
        SetDefaultButtons();
	}

    void SetDefaultButtons()
    {
        if (pauseButton1 == none)
        {
            pauseButton1 = defaultPauseButton1;
        }
        if (pauseButton2 == none)
        {
            pauseButton2 = defaultPauseButton2;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        input = Input.GetKeyDown(pauseButton1) || Input.GetKeyDown(pauseButton2);

        if (input)
        {
            pausePanel.color = Color.Lerp(menuFaded, menuTransparent, 2f);
        }
	}
}
