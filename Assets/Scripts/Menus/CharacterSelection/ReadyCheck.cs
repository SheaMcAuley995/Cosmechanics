using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyCheck : MonoBehaviour
{
    public static ReadyCheck instance;

    public Text countdownToStartText;
    Coroutine countdown;

    [SerializeField] int playersReady = 0;
    [SerializeField] float time = 10;
    bool countingDown;


    #region Singleton
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    void Start()
    {
        countdownToStartText.enabled = false;
    }

    public void IncreasePlayersReady()
    {
        playersReady++;

        if (AllPlayersReady())
        {
            StartCountdown();
        }
    }

    public void DecreasePlayersReady()
    {
        playersReady--;

        if (!AllPlayersReady())
        {
            StopCountdown();
        }
    }

    public bool AllPlayersReady()
    {
        if (playersReady >= ExampleGameController.instance.numberOfPlayers && playersReady != 0)
        {
            return true;
        }
        return false;
    }

    public void StartCountdown()
    {
        time = 4f;
        countingDown = true;
        countdown = StartCoroutine(CountdownToGame());
    }

    public void StopCountdown()
    {
        time = 10;
        //countdown = StartCoroutine(CountdownToGame());
        //StopCoroutine(countdown);
        countingDown = false;
        countdown = null;
        countdownToStartText.enabled = false;
    }

    IEnumerator CountdownToGame()
    {
        countdownToStartText.enabled = true;

        while (countingDown)
        {
            if (time > 0f)
            {
                time -= 1f;
                countdownToStartText.text = "Starting Game In: " + Mathf.RoundToInt(time).ToString();

                yield return new WaitForSeconds(1f);
            }
            else
            {
                PlayerActivation.instance.ContinueToGame();
                break;
            }
        }
    }
}
