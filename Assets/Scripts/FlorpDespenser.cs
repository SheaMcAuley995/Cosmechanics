
using System;
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

    public void InteractWith(){
        dump = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(interactedFlorp != null)
        {
            interactedFlorp = other.GetComponent<Florp>();
            interactedFlorp.doFill = true;
        }
        if (other.GetComponent<Florp>() != null /*&& dump*/)
        {
            other.GetComponent<Florp>().doFill = true;
            DoDump();
        }
        else { dump = false; }
        dump = false;
    }

    private void DoDump()
    {
        if (dump)
        {       
            GameObject part = Instantiate(particle, point, Quaternion.identity);
            part.GetComponent<ParticleSystem>().Play();
            AudioEventManager.instance.PlaySound("splat",.8f,.8f,0);
            dump = false;
        }
    }

    //void OnTriggerExit(Collider other)
    //{
    //    if(interactedFlorp.GetComponent<GameObject>() == other.GetComponent<GameObject>())
    //    {
    //        dump = false;
    //        interactedFlorp.doFill = false;
    //        interactedFlorp = null;
    //    }
    //}

}