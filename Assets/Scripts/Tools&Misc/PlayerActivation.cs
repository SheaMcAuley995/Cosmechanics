using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerActivation : MonoBehaviour
{
    public static PlayerActivation instance = null;

    public PlayerController[] chosenCharacters;

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

    public void ContinueToGame()
    {
        chosenCharacters = GameObject.FindObjectsOfType<PlayerController>();

        for (int numOfChars = 0; numOfChars < chosenCharacters.Length; numOfChars++)
        {
            DontDestroyOnLoad(chosenCharacters[numOfChars]);
        }

        SceneFader.instance.FadeTo("ZachOverWorld");
    }
}
