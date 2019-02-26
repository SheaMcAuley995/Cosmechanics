using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : MonoBehaviour, IInteractable {

    public delegate void myCurrentInteractions();
    //public myCurrentInteractions

    public void InteractWith()
    {
        Debug.Log("Calling interface");
    }
}
