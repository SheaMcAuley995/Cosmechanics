using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForcingStuffLastMinute : MonoBehaviour
{
    PlayerController[] players;
    Scene scene;

	// AHHHHHHHHHHHHHHHHHHHHHHHH
	void Start ()
    {
        players = FindObjectsOfType<PlayerController>();
        scene = SceneManager.GetActiveScene();

        if (scene.name == "Ship_Level_Tutorial NEW")
        {
            ExampleGameController.instance.transform.position = new Vector3(-1f, 0.05f, -4f);
            StartCoroutine(AHHH());
        }
        else
        {
            ExampleGameController.instance.transform.position = new Vector3(-1.81f, 0.05f, -13.5f);
        }

        for (int i = 0; i < players.Length; i++)
        {
            players[i].gameObject.transform.position = ExampleGameController.instance.spawnPoints[i];
        }
	}

    IEnumerator AHHH()
    {
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < players.Length; i++)
        {
            players[i].gameObject.transform.position = new Vector3(-1f, 0.05f, -4f);
        }
        yield return null;
    }
}
