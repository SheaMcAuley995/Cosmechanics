using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public AttackLocation fireLocation;
    ParticleSystem particles;
    ParticleSystem.EmissionModule emiss;

    [Range(1f, 15f)] public float spreadTimer;
    public float fireHealth = 1.75f;
    public int damageToShip = 5;

    public Collider[] colliders;
    public LayerMask waterLayer;

    public Node thisNode;

    float stunTimer = 1.5f;
    float time;


    void Awake()
    {
        particles = GetComponent<ParticleSystem>();
        emiss = GetComponent<ParticleSystem>().emission;
        //ShipHealth.instance.shipCurrenHealth -= damageToShip;
        ShipHealth.instance.AdjustUI();

        StartCoroutine(FireSpread());
    }

    IEnumerator FireSpread()
    {
        while (true)
        {
            yield return new WaitForSeconds(spreadTimer);
            int neighbourIndex = Random.Range(0, fireLocation.nodes.Count);
            if (fireLocation.nodes[neighbourIndex].isFlamable)
            {
                Grid.instance.GenerateLaserFire(fireLocation.nodes[neighbourIndex]);
                fireLocation.nodes[neighbourIndex].isFlamable = false;
                //Debug.Log("Spreading Flames");
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

        if (other.gameObject.CompareTag("Char"))
        {
            do {
                time += 1f * Time.deltaTime;
            } while (time < stunTimer);

            if (time >= stunTimer)
            {
                PlayerController thePlayer = other.GetComponent<PlayerController>();
                StartCoroutine(StunPlayer(thePlayer));
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Char"))
        {
            time = 0f;
        }
    }

    IEnumerator StunPlayer(PlayerController player)
    {
        Debug.LogWarning("DOING THE THING");
        
        //yield return new WaitForSeconds(2f);
        //player.movementVector.x -= 30f;
        yield return null;
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
        thisNode.isFlamable = true;
        Destroy(this.gameObject);
    }
}
