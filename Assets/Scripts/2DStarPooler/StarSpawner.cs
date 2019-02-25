using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawner : MonoBehaviour {

    ObjectPooler objectPooler;
    public Quaternion starRotation;
    public Vector3 direction;
    public float shipSpeed = 1;
    public float spawnSpeed = 0.5f;


    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
        //StartCoroutine("spawnUpdate");
    }

    private void FixedUpdate()
    {
        ObjectPooler.Instance.SpawnFromPool("Stars", transform.position, starRotation, direction, shipSpeed);
    }

   //IEnumerator spawnUpdate()
   //{
   //    while(true)
   //    {
   //        ObjectPooler.Instance.SpawnFromPool("Stars", transform.position, Quaternion.identity, direction, shipSpeed);
   //        yield return new WaitForSeconds(spawnSpeed);
   //    }
   //}
}
