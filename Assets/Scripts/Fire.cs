using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public AttackLocation attackLocation;

    [Range(1f, 15f)] public float spreadTimer;
    public float fireHealth = 5f;
    public int damageToShip = 5;

    List<Node> neighbouringNodes = new List<Node>();


    void Awake()
    {
        ShipHealth.instance.shipCurrenHealth -= damageToShip;
        ShipHealth.instance.AdjustUI();

        attackLocation.worldPositon = gameObject.transform.position;
        Node thisNode = Grid.instance.grid[(int)attackLocation.worldPositon.x, (int)attackLocation.worldPositon.y];
        //attackLocation.nodes = Grid.instance.GetNeighbors(thisNode);
        neighbouringNodes = Grid.instance.GetNeighbors(thisNode);

        StartCoroutine(FireSpread());
    }

    IEnumerator FireSpread()
    {
        while (true)
        {
            yield return new WaitForSeconds(spreadTimer);
            int neighbourIndex = Random.Range(0, neighbouringNodes.Count);
            if (neighbouringNodes[neighbourIndex].isFlamable)
            {
                Grid.instance.GenerateLaserFire(neighbouringNodes[neighbourIndex]);
                neighbouringNodes[neighbourIndex].isFlamable = false;
                Debug.Log("Spreading flames");
            }
        }
    }
}
