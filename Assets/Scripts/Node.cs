using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node  {

    public Collider[] playerArray;
    public BoxCollider box;
    public bool isFlamable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;
    public float fireTimer;
    public GameObject fireEffect;

    public Node(bool _isFlamable, Vector3 _worldPos, int _gridX, int _gridY, float _fireTimer, GameObject _fireEffect, Collider[] _playerArray)
    {
        isFlamable = _isFlamable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
        fireTimer = _fireTimer;
        fireEffect = _fireEffect;
        playerArray = _playerArray;
    }
}
