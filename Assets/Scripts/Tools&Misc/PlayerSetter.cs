using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetter : MonoBehaviour
{
    PlayerController[] players;
    
	void Start ()
    {
        players = FindObjectsOfType<PlayerController>();
        CameraMultiTarget example = FindObjectOfType<CameraMultiTarget>();
        var targets = new List<GameObject>(players.Length);

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].cameraTrans == null)
            {
                targets.Add(players[i].gameObject);
                players[i].cameraTrans = Camera.main.transform;
            }
            players[i].gameObject.transform.position = ExampleGameController.instance.spawnPoints[i];
            Debug.Log(i);
        }

        example.SetTargets(targets.ToArray());

        for (int i = 0; i < players.Length; i++)
        {
            players[i].transform.localScale = new Vector3(1f, 1f, 1f);
            players[i].walkSpeed = 5.5f;
            players[i].runSpeed = 5.5f;
            players[i].turnSmoothTime = 0.05f;
        }
	}
}
