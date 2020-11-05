using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairableObject : MonoBehaviour, IInteractable, IDamageable<int> {
    public int health = 2;

    public int healthMax = 2;

    public int repairAmount = 1;

    [SerializeField] MeshRenderer mesh;
    [SerializeField] MeshFilter filter;
    [SerializeField] Mesh[] meshes;
    int currentMesh;

    public GameObject repairEffect;
    public ParticleSystem steamEffect;
    public ParticleSystem steamEffect2;


    public bool takeDamageDebug = false;

    //AudioSource pipeSound;

    private void Start()
    {
        //pipeSound = GetComponent<AudioSource>();

        if (filter == null) { filter = GetComponent<MeshFilter>(); }
        if (mesh == null) { mesh = GetComponent<MeshRenderer>(); }

        steamEffect.Stop();
        steamEffect2.Stop();
        //StartCoroutine("takeDamage");
    }
    public void InteractWith()
    {
        //Todo: Set up a mechanic that take in the currently equiped tool. 
        if (health < healthMax)
        {
            repairObject(repairAmount);
            mesh.material.color -= Color.red;
            GameObject nutsAndBolts = Instantiate(repairEffect, transform.position + new Vector3(0, 0.1f), Quaternion.identity);
            Destroy(nutsAndBolts.gameObject, 1);

            if (steamEffect.isPlaying)
            {
                steamEffect.Stop();
                steamEffect2.Stop();
            }

        }
    }



    private void Update()
    {
        if (takeDamageDebug)
        {
            TakeDamage(1);
            takeDamageDebug = false;
        }
    }

    public void repairObject(int repairAmount)
    {
        GameplayLoopManager.instance.explosionDamage -= repairAmount;
        currentMesh -= 1;
        filter.mesh = meshes[currentMesh];
        health = health + repairAmount;

    }



    public void TakeDamage(int damageTaken)
    {
        if (health > 0)
        {
            GameplayLoopManager.instance.explosionDamage += damageTaken;
            health -= damageTaken;
            currentMesh += 1;
            filter.mesh = meshes[currentMesh];
            mesh.material.color += Color.red;


            if (!steamEffect.isPlaying)
            {
                steamEffect.Play();
                steamEffect2.Play();
            }

        }

    }

    public void TakeDamage(object explosionDamage)
    {
        throw new System.NotImplementedException();
    }
}
