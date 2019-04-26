using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killTimer : MonoBehaviour {


    private void Start()
    {
        StartCoroutine(killme());
    }
    [Range(.1f, 10)]
    public float killTime;
    
    IEnumerator killme()
    {
        yield return new WaitForSeconds(killTime);
        Destroy(this.gameObject);
    }
}
