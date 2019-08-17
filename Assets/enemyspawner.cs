using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyspawner : MonoBehaviour {

    public Vector3[] spawnPositions;

    public float timeBetweenSpawns;
    public GameObject enemyObject;

    private float countdown;

    private void Update()
    {
        if(countdown <= 0)
        {
            spawnEnemy();
            countdown = timeBetweenSpawns;
        }

        countdown -= Time.deltaTime;
    }

    private void spawnEnemy()
    {
        Instantiate(enemyObject, spawnPositions[Random.Range(0, spawnPositions.Length)], Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        foreach(Vector3 spawnPos in spawnPositions)
        {
            Gizmos.DrawWireSphere(spawnPos, 1);
        }
    }
}
