using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseGame : MonoBehaviour
{
    [Header("UI Elements")]
    public Canvas pauseCanvas;
    public Image[] images;
    public Text[] texts;

    [Header("Configurations")]
    public ButtonSelectionManager selection;
    public KeyCode pauseButton1;
    public KeyCode pauseButton2;
    public string menuScene;

    [Header("Colours")]
    public Color panelStartColor;
    public Color panelEndColor;
    public Color pauseTextStartColor;
    public Color pauseTextEndColor;
    public Color buttonStartColor;
    public Color buttonEndColor;
    public Color buttonTextStartColor;
    public Color buttonTextEndColor;

    [Header("Fade Settings")]
    public float fadeInTime = 1f;
    public float fadeOutTime = 2f;

    Vector3 zero = Vector3.zero;
    Vector3 one = Vector3.one;
    Vector3 fullSize = new Vector3(1.5f, 1.5f, 1.5f);
    bool input, paused;
    bool canCheck = false;

    PlayerController[] playerControllers;

    #region KeyCode caching
    KeyCode defaultPauseButton1 = KeyCode.Escape;
    KeyCode defaultPauseButton2 = KeyCode.P;
    KeyCode none = KeyCode.None;
    #endregion

    // Use this for initialization
    IEnumerator Start ()
    {
        selection.enabled = false;
        SetDefaultButtons();
        playerControllers = FindObjectsOfType<PlayerController>();
        yield return new WaitForSeconds(0.2f);
        canCheck = true;
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

    void DetectInput()
    {
        input = Input.GetKeyDown(pauseButton1) || Input.GetKeyDown(pauseButton2);

        if (input && !paused)
        {
            StopCoroutine(FadeOut(images, texts));
            StartCoroutine(FadeIn(images, texts));
        }

        foreach (PlayerController player in playerControllers)
        {
            player.getInput();

            if (player.pauseButton)
            {
                StopCoroutine(FadeOut(images, texts));
                StartCoroutine(FadeIn(images, texts));
            }

            if (player.pickUp && selection.isActiveAndEnabled)
            {
                ResumeGame();
            }

            if (player.sprint && selection.isActiveAndEnabled)
            {
                QuitGame();
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (canCheck)
        {
            DetectInput();
        }
	}

    IEnumerator FadeIn(Image[] images, Text[] texts)
    {
        selection.enabled = true;
        paused = true;
        Time.timeScale = 0f;

        for (float time = 0.01f; time < fadeInTime; time += 0.1f)
        {
            images[0].color = Color.Lerp(panelStartColor, panelEndColor, time / fadeInTime);
            for (int i = 1; i < images.Length; i++)
            {
                images[i].color = Color.Lerp(buttonStartColor, buttonEndColor, time / fadeInTime);
                images[i].gameObject.transform.localScale = Vector3.Lerp(zero, fullSize, time / fadeInTime);
            }
            
            texts[0].color = Color.Lerp(pauseTextStartColor, pauseTextEndColor, time / fadeInTime);
            texts[0].gameObject.transform.localScale = Vector3.Lerp(zero, one, time / fadeInTime);
            for (int i = 1; i < texts.Length; i++)
            {
                texts[i].color = Color.Lerp(buttonTextStartColor, buttonTextEndColor, time / fadeInTime);
                texts[i].gameObject.transform.localScale = Vector3.Lerp(zero, one, time / fadeInTime);
            }

            yield return null;
        }

        StopCoroutine(FadeIn(images, texts));
    }

    public IEnumerator FadeOut(Image[] images, Text[] texts)
    {
        selection.enabled = false;
        paused = false;

        for (float time = 0.01f; time < fadeOutTime; time += 0.1f)
        {
            images[0].color = Color.Lerp(panelEndColor, panelStartColor, time / fadeOutTime);
            for (int i = 1; i < images.Length; i++)
            {
                images[i].color = Color.Lerp(buttonEndColor, buttonStartColor, time / fadeOutTime);
                images[i].gameObject.transform.localScale = Vector3.Lerp(fullSize, zero, time / fadeOutTime);
            }

            texts[0].color = Color.Lerp(pauseTextEndColor, pauseTextStartColor, time / fadeOutTime);
            texts[0].gameObject.transform.localScale = Vector3.Lerp(one, zero, time / fadeOutTime);
            for (int i = 1; i < texts.Length; i++)
            {
                texts[i].color = Color.Lerp(buttonTextEndColor, buttonTextStartColor, time / fadeOutTime);
                texts[i].gameObject.transform.localScale = Vector3.Lerp(one, zero, time / fadeOutTime);
            }

            yield return null;
        }

        Time.timeScale = 1f;
        StopCoroutine(FadeOut(images, texts));
    }

    public void ResumeGame()
    {
        if (paused)
        {
            StopCoroutine(FadeIn(images, texts));
            StartCoroutine(FadeOut(images, texts));
        }
    }

    public void OptionsMenu()
    {
        Debug.Log("Displaying Options Menu!");
    }

    public void QuitGame()
    {
        ResumeGame();
        SceneFader.instance.FadeTo("MainMenu_Update");
    }
}