using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class SetSpawnPositions : MonoBehaviour
{
    PauseMenu pauseMenu;
    public GameObject inGameCanvas;
    public Vector3[] spawnpositions = new Vector3[4];
    GameObject[] players;

    private IEnumerator Start()
    {
        CharacterHandler.instance.spawnPoints = spawnpositions;

        yield return new WaitForEndOfFrame();

        players = CharacterHandler.instance.players;

        yield return null;
    }

    private void Update()
    {
        if (players != null)
        {
            foreach (GameObject player in players)
            {
                PlayerController playerScript = player.GetComponent<PlayerController>();
                if (playerScript.player.GetButtonDown("Pause"))
                {
                    pauseMenu = FindObjectOfType<PauseMenu>();

                    if (playerScript != null)
                    {
                        pauseMenu.PauseGame(pauseMenu.pause);
                    }
                    else
                    {
                        Debug.Log("Pause Menu is Missing");
                    }
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < 4; i++)
        {
            Gizmos.DrawSphere(spawnpositions[i], 1);
        }
    }
}
