using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomGuyGrab : MonoBehaviour {


    GameObject thing;
    public Transform holdPoint;
	// Use this for initialization


    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Object")
        {
            thing = other.gameObject;
            Debug.Log("hey we got a thing");
            other.transform.position = holdPoint.position;
            
            Destroy(other.GetComponent<Rigidbody>());
        }
    }
    private void Update()
    {
        if (thing!=null)
        {
            thing.transform.position = holdPoint.transform.position;
        }
        
        //foreach (var item in collection)
        //{
        //    for (int i = 0; i < length; i++)
        //    {
        //        while (true)
        //        {
        //            if (true)
        //            {
        //                do
        //                {
        //                   FindObjectsOfTypeAll(Object);
        //                } while (true);
        //            }
        //        }
        //    }
        //}
    }

}
