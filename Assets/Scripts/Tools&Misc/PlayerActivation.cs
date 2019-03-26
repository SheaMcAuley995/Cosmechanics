using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerActivation : MonoBehaviour
{
    public static PlayerActivation instance = null;

    public GameObject[] chosenCharacters;

    int currentPlayerId = 0;

	// Use this for initialization
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
	
	public void ContinueToGame()
    {
        chosenCharacters = GameObject.FindGameObjectsWithTag("Char");

        for (int numOfChars = 0; numOfChars < chosenCharacters.Length; numOfChars++)
        {
            DontDestroyOnLoad(chosenCharacters[numOfChars]);

            chosenCharacters[numOfChars].GetComponent<PlayerController>().playerId = currentPlayerId;
            currentPlayerId++;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
