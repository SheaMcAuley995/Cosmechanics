
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlorpDespenser : MonoBehaviour , IInteractable {


    public void InteractWith(){
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Florp>() != null)
        {
            other.GetComponent<Florp>().doFill = true;
        }
    }

}











//public GameObject florpPrefab;
//public static FlorpDespenser instance;
//public Transform florpEjection;
//[SerializeField]private float lerpTime = 1f;

//public float speed = 20f;
//GameObject dispensedFlorp;

//public void InteractWith()
//{ 
//       GiveFlorp();   

//}


//private void GiveFlorp()
//{

//    dispensedFlorp = Instantiate(florpPrefab, florpEjection.position + new Vector3(0,2,0), Quaternion.identity);
//    AudioEventManager.instance.PlaySound("splat", .3f, Random.Range(.9f, 1f), 0);


//}