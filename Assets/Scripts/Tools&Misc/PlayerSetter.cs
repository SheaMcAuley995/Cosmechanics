using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetter : MonoBehaviour
{
    PlayerController[] players;
    
	void Start ()
    {
        //yield return new WaitForSeconds(0.2f);
        players = FindObjectsOfType<PlayerController>();
        CameraMultiTarget example = FindObjectOfType<CameraMultiTarget>();
        var targets = new List<GameObject>(players.Length);

        ExampleGameController.instance.setSpawnPoints();

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].cameraTrans == null)
            {
                targets.Add(players[i].gameObject);
                players[i].cameraTrans = Camera.main.transform;
            }
            players[i].gameObject.transform.position = ExampleGameController.instance.spawnPoints[i];
        }

        example.SetTargets(targets.ToArray());

        for (int i = 0; i < players.Length; i++)
        {
            players[i].enabled = true;
            players[i].gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }
	}
}
