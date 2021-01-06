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
    [SerializeField] GameObject testPrefab;


    private IEnumerator Start()
    {
        
        CharacterHandler.instance.spawnPoints = spawnpositions;

        yield return new WaitForEndOfFrame();

        players = CharacterHandler.instance.players;

        pauseMenu = FindObjectOfType<PauseMenu>();
        
        yield return null;
    }

    private void Update()
    {
        if (players != null)
        {
            foreach (GameObject player in players)
            {
                PlayerController playerScript = player.GetComponent<PlayerController>();
                if (playerScript != null)
                {
                    if (playerScript.player.GetButtonDown("Pause"))
                    {
                        pauseMenu.PauseGame(pauseMenu.pause);
                    }
                    playerScript.pause = pauseMenu.pause;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        for (int i = 0; i < 4; i++)
        {
            Gizmos.DrawWireSphere(spawnpositions[i], 1);
            Gizmos.DrawSphere(spawnpositions[i], 0.2f);
        }
    }
}
