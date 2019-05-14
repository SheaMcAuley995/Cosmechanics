using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleDispenser : MonoBehaviour, IInteractable {

    public GameObject bottlePrefab;
    public static BottleDispenser instance;
    public Transform bottleEjection;
    [SerializeField] private float lerpTime = 1f;
    [HideInInspector] public int bottlesDispensed = 0;
    [HideInInspector] public bool shouldDispense = true;
    public float speed = 20f;
    GameObject dispensedBottle;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void InteractWith()
    {
        GiveFlorp(); //this is what gives florp

    }


    private void GiveFlorp()
    {
        if (bottlesDispensed <= 10)
        {
            dispensedBottle = Instantiate(bottlePrefab, bottleEjection.position + new Vector3(0, 2, 0), Quaternion.identity);
            AudioEventManager.instance.PlaySound("bottledrop", .3f, Random.Range(.9f, 1f), 0);
            bottlesDispensed++;
        }
    }

}
