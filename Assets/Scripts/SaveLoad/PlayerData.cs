using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

    public int level;
    public int health;
    public float[] position;

    public PlayerData(Paalayer player)
    {
        level = player.level;
        health = player.health;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[0] = player.transform.position.y;
        position[0] = player.transform.position.z;

    }
}
