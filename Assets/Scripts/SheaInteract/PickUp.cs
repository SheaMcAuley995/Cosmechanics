using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PickUp : MonoBehaviour {

    public Rigidbody rb;
    //public GameObject PUU_ShaderPrefab;
   // GameObject puu;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public virtual void pickMeUp(Transform pickUpTransform)
    {
        rb.isKinematic = true;
        transform.SetParent(pickUpTransform);
        transform.position = pickUpTransform.position;
        transform.eulerAngles = pickUpTransform.eulerAngles;
        //puu = Instantiate(PUU_ShaderPrefab, pickUpTransform.position, Quaternion.identity, pickUpTransform);
       
    }
   
    public void putMeDown()
    {
        if (transform.gameObject.GetComponent<FireExtinguisher>())
        {
            transform.gameObject.GetComponent<FireExtinguisher>().StopExtinguisher();
        }
        transform.SetParent(null);
        rb.isKinematic = false;
       // Destroy(puu);
    }
    //private void OnDestroy()
    //{
    //   // Destroy(puu);
    //}
}
