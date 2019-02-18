using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
public class TempPipe : Interactable {

    /// <summary>
    /// InteractWith() is a script that will run once when a player walks next to something and presses A
    /// Interactable has an update but you have to throw anything you want to run into it on myCurrentInteractions by saying myCurrentInteractions += the funtion you want to run in update
    /// I want you to forget about this script
    /// </summary>


    // [SerializeField] float timerMax;
    // float timer;
    //
    // bool crackedPipe;

    // private void Start()
    // {
    //     timer = timerMax;
    //     //myRigidbody = GetComponent<Rigidbody>();
    //     StartCoroutine("startClock");
    // }

    public override void InteractWith()
    {
        Debug.Log("I'm being interacted with!");
        PickUp();
        base.InteractWith();
    }

   // IEnumerator startClock()
   // {
   //     if(!crackedPipe)
   //     {
   //         yield return new WaitForSeconds(timer);
   //     }
   // }
}
