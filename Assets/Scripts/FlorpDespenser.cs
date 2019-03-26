
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlorpDespenser : MonoBehaviour , IInteractable {

    public GameObject florpPrefab;
    public static FlorpDespenser instance;
    public Transform florpEjection;

    public float speed = 20f;

    public void InteractWith()
    {
        Debug.Log("we got here");
           GiveFlorp();
        
        
    }

    private void GiveFlorp()
    {
        GameObject florp = Instantiate(florpPrefab, transform.position + new Vector3(0,2,0), Quaternion.identity);
        AudioEventManager.instance.PlaySound("splat", .3f, Random.Range(.9f, 1f), 0);
        float up = florp.GetComponent<Rigidbody>().velocity.y;
        up += speed;
    }
}
