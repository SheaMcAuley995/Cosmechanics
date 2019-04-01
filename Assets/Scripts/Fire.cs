using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public AttackLocation fireLocation;
    ParticleSystem particles;
    ParticleSystem.EmissionModule emiss;

    [Range(1f, 15f)] public float spreadTimer;
    public float fireHealth = 3f;
    public int damageToShip = 5;

    List<Node> neighbouringNodes = new List<Node>();
    public Collider[] colliders;
    public LayerMask waterLayer;

    Node thisNode;


    void Awake()
    {
        particles = GetComponent<ParticleSystem>();
        emiss = GetComponent<ParticleSystem>().emission;
        ShipHealth.instance.shipCurrenHealth -= damageToShip;
        ShipHealth.instance.AdjustUI();

        fireLocation.worldPositon = gameObject.transform.position;
        //thisNode = Grid.instance.grid[(int)fireLocation.worldPositon.x, (int)fireLocation.worldPositon.y];
        //attackLocation.nodes = Grid.instance.GetNeighbors(thisNode);
        //neighbouringNodes = Grid.instance.GetNeighbors(thisNode);

        //StartCoroutine(FireSpread());
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

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Extinguisher") && other.GetComponent<FireExtinguisher>().isExtinguishing)
        {
            TakeDamage(1f * Time.deltaTime);
            //emiss.rateOverTime = Mathf.Lerp(10f, 0f, 100f / Time.deltaTime);
        }
    }

    public void TakeDamage(float amount)
    {
        fireHealth -= amount;

        if (fireHealth <= 0f)
        {
            Extinguish();
        }
    }

    void Extinguish()
    {
        //thisNode.isFlamable = true;
        Destroy(this.gameObject);
    }
}
