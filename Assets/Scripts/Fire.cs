using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public AttackLocation fireLocation;
    public GameObject fireOnPlayerEffect;
    ParticleSystem particles;
    ParticleSystem.EmissionModule emiss;

    [Range(1f, 15f)] public float spreadTimer;
    public float fireHealth = 1.75f;
    public int damageToShip = 5;

    public Collider[] colliders;
    public LayerMask waterLayer;

    public Node thisNode;

    [SerializeField] float timeUntilStun = 1.5f;
    [SerializeField] float stunTime = 3f;
    float time = 0f;


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
            time += 1f * Time.deltaTime;

            if (time >= timeUntilStun) // 3 seconds
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

    IEnumerator StunPlayer(PlayerController controller)
    {
        GameObject playerFire = Instantiate(fireOnPlayerEffect, controller.transform.position, Quaternion.identity, controller.transform);
        time = 0f;
        //controller.normalMovement = false;
        yield return new WaitForSeconds(stunTime); // 3 seconds
        //controller.normalMovement = true;
        Destroy(playerFire);
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
