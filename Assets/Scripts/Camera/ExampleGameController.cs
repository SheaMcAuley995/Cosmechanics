using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExampleGameController : MonoBehaviour
{

    /// <summary>
    /// In here I want the players to be assigned a different material based on what player they are. Once you are done with that if you could work
    /// on a very bare bones main menu that lets you choose how many players are going to be in the game after you press play. Or don't make it bare bones and just 
    /// go really hard. Bonus points if the players get to choose their material. BONUS BONUS POINTS if you work with zach and get this working with his character generator.
    /// </summary>
    public Material[] materials;
    public int numberOfPlayers;
    public CameraMultiTarget cameraMultiTarget;
    public GameObject playerPrefab;
    private int currentPlayerId = 0;
    private Vector3[] spawnPoints;

    private void OnValidate()
    {
        setSpawnPoints();
    }

    private void Start()
    {
        MakePlayers(numberOfPlayers);
    }

    private void MakePlayers(int numberOfPlayers) {

        var targets = new List<GameObject>(numberOfPlayers);

        for (int i = 0; i < numberOfPlayers; i++)
        {

            targets.Add(addPlayer());
            cameraMultiTarget.SetTargets(targets.ToArray());
        }
    }

    public void setSpawnPoints()
    {
        spawnPoints = new Vector3[numberOfPlayers];
        spawnPoints[0] = transform.position;
        for(int i = 0; i < numberOfPlayers; i++)
        {
            spawnPoints[i] = transform.position + new Vector3(i + 1, 0, 0);
        }

    }
    

    public GameObject addPlayer() {
        GameObject target = GameObject.Instantiate(playerPrefab,spawnPoints[currentPlayerId],Quaternion.identity);
        target.GetComponent<MeshRenderer>().material = materials[currentPlayerId];
        target.GetComponent<PlayerController>().playerId = currentPlayerId;
        target.GetComponent<PlayerController>().cameraTrans = cameraMultiTarget.GetComponent<Camera>().transform;
        currentPlayerId++;
        return target;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        for(int i = 0; i < numberOfPlayers; i++ )
        {
            Gizmos.DrawSphere(transform.position + new Vector3(i + 1, 0, 0), 0.5f);
        }
        
        
    }

}