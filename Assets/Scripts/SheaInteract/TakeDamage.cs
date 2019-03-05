using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour, IInteractable {

    private int health = 100;
    private float repairTimer;

    public int healthMax = 100;
    public float repairTimerMax = 1;
    public int repairAmount = 25;

    MeshRenderer mesh;

    private void Start()
    {
        repairTimer = repairTimerMax;
        mesh = GetComponent<MeshRenderer>();
        StartCoroutine("takeDamage");
    }
    public void InteractWith()
    {
        repairTimer = repairTimerMax;
        //Todo: Set up a mechanic that take in the currently equiped tool. 
        if (health < healthMax)
        {
            repairObject(repairAmount);
            mesh.material.color -= Color.red;
            Debug.Log("Health Points : " + health);

        }
    }

    public void repairObject(int repairAmount)
    {
        health = Mathf.Clamp(health + repairAmount, 0, healthMax);
    }

    IEnumerator takeDamage()
    {
        while(true)
        {
            if(health > 0)
            {
                repairTimer -= Time.deltaTime;

                if (repairTimer < 0)
                {
                    repairObject(-repairAmount);

                    if (health > 0)
                        mesh.material.color += Color.red;

                    Debug.Log("Health Points : " + health);

                    repairTimer = repairTimerMax;
                }
            }
           
            yield return null;
        }
    }
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
}
