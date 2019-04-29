using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerActivation : MonoBehaviour
{
    public static PlayerActivation instance = null;

    public SelectedPlayer[] chosenCharacters;

    #region Singleton
    void Awake ()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ContinueToGame()
    {
        chosenCharacters = GameObject.FindObjectsOfType<SelectedPlayer>();

        for (int numOfChars = 0; numOfChars < chosenCharacters.Length; numOfChars++)
        {
            DontDestroyOnLoad(chosenCharacters[numOfChars]);
        }

        SceneFader.instance.FadeTo("ZachOverWorld");
    }

    public void RespawnPlayers()
    {
        for (int numOfChars = 0; numOfChars < chosenCharacters.Length; numOfChars++)
        {
            DontDestroyOnLoad(chosenCharacters[numOfChars]);
        }
    }
}
