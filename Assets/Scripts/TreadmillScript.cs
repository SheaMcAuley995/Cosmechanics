using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreadmillScript : MonoBehaviour
{
    public float speed;
    public bool reverse;
    public bool turning;

    private void OnTriggerStay(Collider other)
    {
        if (turning)
        {
            other.transform.position += transform.right * (Time.deltaTime * speed/4);
            other.transform.rotation = new Quaternion(other.transform.rotation.x, other.transform.rotation.y, other.transform.rotation.z, other.transform.rotation.w + (1 * (Time.deltaTime * speed / 2)));
        }
        else if(turning == true && reverse == true)
        {
            other.transform.position += transform.right * (Time.deltaTime * speed / 4);
            other.transform.rotation = new Quaternion(other.transform.rotation.x, other.transform.rotation.y, other.transform.rotation.z, other.transform.rotation.w + (1 * (Time.deltaTime * speed / 2)));
        }
        else
        {
            if (reverse)
            {
                other.transform.position -= transform.forward * (Time.deltaTime * speed);
            }
            else
            {
                other.transform.position += transform.forward * (Time.deltaTime * speed);
            }
        }
        
    }
}
