using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Rewired;

public class LevisSceneTransition : MonoBehaviour
{


   public void LoadScene(string name)
    {
        StartCoroutine(CharacterHandler.instance.Transition(name));
    }

    //private IEnumerator Transition(string name)
    //{
    //    DontDestroyOnLoad(gameObject);
    //
    //    yield return SceneManager.LoadSceneAsync(name);
    //    print(name + " has been loaded successfully");        
    //
    //    if(name.Contains("Ship_Level"))
    //    {
    //        int j = 0;
    //        foreach (PlayerController player in FindObjectsOfType<PlayerController>())
    //        {
    //            players[j] = player.gameObject;
    //            j++;
    //        }
    //
    //        spawnpoints = FindObjectOfType<SetSpawnPositions>().spawnpositions;
    //        CameraMultiTarget.instance.SetTargets(players);
    //        
    //        for (int i = 0; i < CharacterHandler.instance.numberOfPlayers; i++)
    //        {
    //            players[i].transform.position = spawnpoints[i];
    //            players[i].GetComponent<PlayerController>().enabled = true;
    //            players[i].GetComponent<PlayerController>().cameraTrans = CameraMultiTarget.instance.GetComponent<Camera>().transform;
    //        }
    //    }
    //
    //
    //    //Destroy(gameObject);
    //}

}
