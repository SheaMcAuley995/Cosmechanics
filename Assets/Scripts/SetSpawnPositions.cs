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

    void Start()
    {
        CharacterHandler.instance.spawnPoints = spawnpositions;

        players = CharacterHandler.instance.players;

        foreach (GameObject player in players)
        {
            pauseMenu = FindObjectOfType<PauseMenu>();
            if (pauseMenu != null)
            {
                player.GetComponent<PlayerController>().player.AddInputEventDelegate(pauseMenu.GetComponent<PauseMenu>().OnPauseUpdate, UpdateLoopType.Update, "Pause");
            }
            else
            {
                Debug.Log("Pause Menu is Missing");
            }
        }
    }

    private void OnDrawGizmos()
    {
        for(int i = 0; i < 4; i++)
        {
            Gizmos.DrawSphere(spawnpositions[i], 1);
        }
    }
}
