using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairableObject : MonoBehaviour, IInteractable, IDamageable<int> {

    public int health = 2;

    public int healthMax = 2;

    public int repairAmount = 1;

    MeshRenderer mesh;
    MeshFilter filter;
    [SerializeField]Mesh[] meshes;
    int currentMesh;

    public GameObject particleEffectPrefab;
    public AlertUI alertUI;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        ShipHealth.instance.shipMaxHealth += healthMax;
        filter = GetComponent<MeshFilter>();
        alertUI.problemMax += healthMax;
        alertUI.problemCurrent += healthMax;
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
             
            AudioEventManager.instance.PlaySound("clang", .3f, Random.Range(.9f,1f), 0);    //play clang audio
           //ShipHealth.instance.shipCurrenHealth += repairAmount;
           // Debug.Log("Health Points : " + health);

        }
    }

   

    public void repairObject(int repairAmount)
    {
        currentMesh -= 1;
        filter.mesh = meshes[currentMesh];
        health = health + repairAmount;
        alertUI.problemCurrent += repairAmount;
        ShipHealth.instance.shipCurrenHealth += repairAmount;
        ShipHealth.instance.AdjustUI();
        // health = Mathf.Clamp(health + repairAmount, 0, healthMax);
    }



    public void TakeDamage(int damageTaken)
    {
        if (health > 0)
        {
            health -= damageTaken;
            currentMesh += 1;
            filter.mesh = meshes[currentMesh];
            mesh.material.color += Color.red;
            alertUI.problemCurrent -= damageTaken;
            ShipHealth.instance.shipCurrenHealth -= damageTaken;
            ShipHealth.instance.AdjustUI();
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
