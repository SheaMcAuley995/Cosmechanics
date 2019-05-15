using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerActivation : MonoBehaviour
{
    public static PlayerActivation instance = null;

    public SelectedPlayer[] chosenCharacters;
    Animator[] animators;

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
        chosenCharacters = FindObjectsOfType<SelectedPlayer>();

        for (int numOfChars = 0; numOfChars < chosenCharacters.Length; numOfChars++)
        {
            DontDestroyOnLoad(chosenCharacters[numOfChars]);

            animators = chosenCharacters[numOfChars].GetComponentsInChildren<Animator>();
            for (int i = 0; i < animators.Length; i++)
            {
                animators[i].SetBool("CharSelect", false);
            }
        }

        SceneFader.instance.FadeTo("ZachOverWorld");
    }
}
