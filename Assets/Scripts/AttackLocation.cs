using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackLocation  {
    public Vector3 worldPositon;
    public List<Node> nodes = new List<Node>();
}
