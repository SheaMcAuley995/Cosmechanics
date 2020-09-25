using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpawnPositions : MonoBehaviour
{

    public Vector3[] spawnpositions = new Vector3[4];
    void Start()
    {
        CharacterHandler.instance.spawnPoints = spawnpositions;
    }

    private void OnDrawGizmos()
    {
        for(int i = 0; i < 4; i++)
        {
            Gizmos.DrawSphere(spawnpositions[i], 1);
        }
    }
}
