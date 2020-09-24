using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPositions : MonoBehaviour
{
    public Vector3[] spawnLocations = new Vector3[4];

    // key = playerID, value = spawn position.
    public Dictionary<int, Vector3> spawnManager;

    public SpawnPositions()
    {
        for (int i = 0; i < 3; i++)
        {
            if (spawnManager.ContainsKey(i))
            {
                // spawn players at matched value for position.
                // Ex: GameObject player = Instantiate(PlayerObject, spawnManager.TryGetValue(i, out spawnLocations[i]));

            }
        }
    }

    // Display spawn positions
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        for (int i = 0; i < spawnLocations.Length; i++)
        {
            Gizmos.DrawSphere(spawnLocations[i], 0.25f);
        }
    }
}
