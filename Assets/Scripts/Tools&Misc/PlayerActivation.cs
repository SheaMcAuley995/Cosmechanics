using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerActivation : MonoBehaviour
{
    public static PlayerActivation instance = null;

    public GameObject[] chosenCharacters;

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
        chosenCharacters = GameObject.FindGameObjectsWithTag("Char");

        for (int numOfChars = 0; numOfChars < chosenCharacters.Length; numOfChars++)
        {
            DontDestroyOnLoad(chosenCharacters[numOfChars]);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        for (int chars = 0; chars < chosenCharacters.Length; chars++)
        {
            chosenCharacters[chars].transform.position = ExampleGameController.instance.spawnPoints[chars];
        }
    }
}
