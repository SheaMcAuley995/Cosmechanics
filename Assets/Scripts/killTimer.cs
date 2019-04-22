using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killTimer : MonoBehaviour {


    private void Start()
    {
        StartCoroutine(killme());
    }
    [Range(2, 10)]
    public int killTime;
    
    IEnumerator killme()
    {
        yield return new WaitForSeconds(killTime);
        Destroy(this.gameObject);
    }
}
