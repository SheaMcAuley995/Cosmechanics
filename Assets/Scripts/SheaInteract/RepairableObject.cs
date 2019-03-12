using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairableObject : MonoBehaviour, IInteractable, IDamageable<int> {

    private int health = 100;

    private AudioEventManager AEV;
    public int healthMax = 100;

    public int repairAmount = 25;

    MeshRenderer mesh;

    public GameObject particleEffectPrefab;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        //StartCoroutine("takeDamage");
    }
    public void InteractWith()
    {
        //Todo: Set up a mechanic that take in the currently equiped tool. 
        if (health < healthMax)
        {
            repairObject(repairAmount);
            mesh.material.color -= Color.red;
            GameObject nutsAndBolts = Instantiate(particleEffectPrefab, transform.position + new Vector3(0,0.1f),Quaternion.identity);
            Destroy(nutsAndBolts.gameObject, 1);
            AEV.PlaySound("clang");                         //plays sound
           // Debug.Log("Health Points : " + health);

        }
    }

    public void repairObject(int repairAmount)
    {
        health = Mathf.Clamp(health + repairAmount, 0, healthMax);
    }

    public void TakeDamage(int damageTaken)
    {
        if (health > 0)
        {
           repairObject(-damageTaken);

           mesh.material.color += Color.red;

           //Debug.Log("Health Points : " + health);

        }
    }
    

    // IEnumerator takeDamage()
    // {
    //     //while(true)
    //     //{
    //         if(health > 0)
    //         {
    //             repairTimer -= Time.deltaTime;
    //             
    //             if (repairTimer < 0)
    //             {
    //                 repairObject(-repairAmount);
    //
    //                 if (health > 0)
    //                     mesh.material.color += Color.red;
    //
    //                 Debug.Log("Health Points : " + health);
    //
    //                 repairTimer = repairTimerMax;
    //             }
    //         }
    //        
    //         yield return null;
    //    // }
    // }
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
}
