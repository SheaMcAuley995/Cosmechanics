
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlorpDespenser : MonoBehaviour , IInteractable {



    public Transform lockPosition;
    public GameObject[] switches = new GameObject[2];

    public Florp interactedFlorp;
    public GameObject particle;
    private ParticleSystem particleFlorp;
    public Transform dispensePoint;
    Vector3 point;
    [SerializeField]
    bool dump = false;

    private void Start()
    {
        particle = Instantiate(particle, point, Quaternion.identity);
        particleFlorp = particle.GetComponent<ParticleSystem>();
        dump = false;
        point = dispensePoint.position;
    }

    public void InteractWith()
    {
        if(interactedFlorp == null)
        {

        }
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
        particleFlorp.Play();

        dump = false;

        if (dump)
        {
            GameObject part = Instantiate(particle, point, Quaternion.identity);
            part.GetComponent<ParticleSystem>().Play();
            
            dump = false;
        }
    }


}
