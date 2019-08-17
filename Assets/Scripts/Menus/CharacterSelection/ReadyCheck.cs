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

        if (playersReady >= ExampleGameController.instance.numberOfPlayers)
        {
            time = 4f;
            countdown = StartCoroutine(CountdownToGame());
        }
    }

    public void DecreasePlayersReady()
    {
        playersReady--;

        if (playersReady < ExampleGameController.instance.numberOfPlayers)
        {
            StopCoroutine(countdown);
            countdownToStartText.enabled = false;
            time = 10;
        }
    }

    IEnumerator CountdownToGame()
    {
        countdownToStartText.enabled = true;

        while (true)
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
