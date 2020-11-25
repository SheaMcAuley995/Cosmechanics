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
        CharacterHandler.instance.players = new GameObject[chosenCharacters.Length];
        for (int i = 0; i < chosenCharacters.Length; i++)
        {
            DontDestroyOnLoad(chosenCharacters[i]);
            CharacterHandler.instance.players[i] = chosenCharacters[i].gameObject;
            animators = chosenCharacters[i].GetComponentsInChildren<Animator>();
            for (int j = 0; j < animators.Length; j++)
            {
                animators[j].Play("CharSelect Idle", -1, 0.8f);
                animators[j].SetBool("CharSelect", false);
            }
   
            //chosenCharacters[i].GetComponent<PlayerController>().enabled = true;
        }
   
        SceneFader.instance.FadeTo("LevelSelectUpdated");
    }
}
