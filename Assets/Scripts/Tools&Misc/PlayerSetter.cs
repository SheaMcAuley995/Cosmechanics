using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetter : MonoBehaviour
{
    PlayerController[] players;

	IEnumerator Start ()
    {
        yield return new WaitForEndOfFrame();
        players = FindObjectsOfType<PlayerController>();
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].cameraTrans == null)
            {
                players[i].cameraTrans = Camera.main.transform;
                players[i].transform.position = ExampleGameController.instance.spawnPoints[i];
            }
        }
	}
}
