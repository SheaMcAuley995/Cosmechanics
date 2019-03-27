using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node : MonoBehaviour  {

    public BoxCollider box;
    public bool isFlamable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;

    public Node(bool _isFlamable, Vector3 _worldPos, int _gridX, int _gridY)
    {
        isFlamable = _isFlamable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }
}
