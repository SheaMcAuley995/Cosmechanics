using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Rewired;

public class LevisSceneTransition : MonoBehaviour
{
    public Vector3[] spawnpoints;
    public GameObject[] players;

   public void LoadScene(string name)
    {
        StartCoroutine(Transition(name));
    }

    private IEnumerator Transition(string name)
    {
        DontDestroyOnLoad(gameObject);

        yield return SceneManager.LoadSceneAsync(name);
        print(name + " has been loaded successfully");

        spawnpoints = FindObjectOfType<SetSpawnPositions>().spawnpositions;

        players = CharacterHandler.instance.players;
        for (int i = 0; i < CharacterHandler.instance.numberOfPlayers; i++)
        {
            players[i].transform.position = spawnpoints[i];
            players[i].GetComponent<PlayerController>().enabled = true;
            players[i].GetComponent<PlayerController>().cameraTrans = CameraMultiTarget.instance.GetComponent<Camera>().transform;
            
        }
        CameraMultiTarget.instance.SetTargets(CharacterHandler.instance.players);
        Destroy(gameObject);
    }

}
