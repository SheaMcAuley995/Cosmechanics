using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broom : Interactable {

    public Transform point;
    public float power;
   
    void Sweep(Vector3 sweepPoint, float sweepPower)
    {
        Collider[] sweepables = Physics.OverlapSphere(sweepPoint, sweepPower);
        foreach (var item in sweepables)
        {

            Debug.Log("gotcha");
        }
    
    }
    public override void InteractWith()
    {
        StartCoroutine(PickupUpdate());
        Sweep(point.position,power);
        base.InteractWith();
    }
}
