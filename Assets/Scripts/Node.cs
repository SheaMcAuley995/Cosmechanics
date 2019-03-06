using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour  {

    public bool walkable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;

    public bool isFlamable;

    public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(0.8f,0.8f,0.8f));
    }
}
