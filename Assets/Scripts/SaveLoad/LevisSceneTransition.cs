using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Rewired;

public class LevisSceneTransition : MonoBehaviour
{
    public Vector3[] spawnpoints;
    public GameObject[] players = new GameObject[CharacterHandler.instance.numberOfPlayers];

   public void LoadScene(string name)
    {
        StartCoroutine(Transition(name));
    }

    private IEnumerator Transition(string name)
    {
        DontDestroyOnLoad(gameObject);

        int j = 0;
        foreach(PlayerController player in FindObjectsOfType<PlayerController>())
        {
            players[j] = player.gameObject;
            j++;
        }
        
        yield return SceneManager.LoadSceneAsync(name);
        print(name + " has been loaded successfully");        

        spawnpoints = FindObjectOfType<SetSpawnPositions>().spawnpositions;


        for(int i = 0; i < CharacterHandler.instance.numberOfPlayers; i++)
        {
            players[i].transform.position = spawnpoints[i];
            players[i].GetComponent<PlayerController>().enabled = true;
            players[i].GetComponent<PlayerController>().cameraTrans = CameraMultiTarget.instance.GetComponent<Camera>().transform;
        }

        Destroy(gameObject);
    }

}
