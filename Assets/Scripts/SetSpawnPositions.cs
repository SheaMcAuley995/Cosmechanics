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

        foreach (GameObject player in players)
        {
            pauseMenu = FindObjectOfType<PauseMenu>();
            PlayerController playerScript = player.GetComponent<PlayerController>();

            if (playerScript != null)
            {              
                 playerScript.player.AddInputEventDelegate(pauseMenu.OnPauseUpdate, UpdateLoopType.Update, "Pause");               
            }
            else
            {
                Debug.Log("Pause Menu is Missing");
            }
        }
        yield return null;
    }

    private void OnDrawGizmos()
    {
        for(int i = 0; i < 4; i++)
        {
            Gizmos.DrawSphere(spawnpositions[i], 1);
        }
    }
}
