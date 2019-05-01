using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleDispenser : MonoBehaviour, IInteractable {

    public GameObject bottlePrefab;
    
    public Transform bottleEjection;
    [SerializeField] private float lerpTime = 1f;

    public float speed = 20f;
    GameObject dispensedBottle;

    public void InteractWith()
    {
        GiveFlorp(); //this is what gives florp

    }


    private void GiveFlorp()
    {

        dispensedBottle = Instantiate(bottlePrefab, bottleEjection.position + new Vector3(0, 2, 0), Quaternion.identity);
        AudioEventManager.instance.PlaySound("bottledrop", .3f, Random.Range(.9f, 1f), 0);


    }

}
