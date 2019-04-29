using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node  {

    public BoxCollider box;
    public bool isFlamable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;
    public float fireTimer;

    public Node(bool _isFlamable, Vector3 _worldPos, int _gridX, int _gridY, float _fireTimer)
    {
        isFlamable = _isFlamable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
        fireTimer = _fireTimer;
    }
}
