
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlorpDespenser : MonoBehaviour , IInteractable {

    Florp interactedFlorp;
    public GameObject particle;
    public Transform dispensePoint;
    Vector3 point;
    [SerializeField]
    bool dump = false;
    private void Start()
    {
        dump = false;
        point = dispensePoint.position;
    }

    public void InteractWith()
    {
        dump = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (interactedFlorp != null)
        {
            interactedFlorp = other.GetComponent<Florp>();
            interactedFlorp.doFill = true;
        }
        if (other.GetComponent<Florp>() != null /*&& dump*/)
        {
            other.GetComponent<Florp>().doFill = true;
            other.GetComponent<Florp>().toolInteraction();
            AudioEventManager.instance.PlaySound("splat", .7f, .8f, 0);
            DoDump();
        }
        else { dump = false; }
        dump = false;
    }

    private void DoDump()
    {
        GameObject uh = Instantiate(particle, point, Quaternion.identity);
        uh.GetComponent<ParticleSystem>().Play();

        dump = false;

        if (dump)
        {
            GameObject part = Instantiate(particle, point, Quaternion.identity);
            part.GetComponent<ParticleSystem>().Play();
            
            dump = false;
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