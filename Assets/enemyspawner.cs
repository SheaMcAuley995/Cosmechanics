using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyspawner : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}



   // IEnumerator CountdownToSpawn()
   // {
   //     countingDown = true;
   //     countdownToStartText.enabled = true;
   //
   //     while (true)
   //     {
   //         if (time > 0f)
   //         {
   //             time -= 1f;
   //             countdownToStartText.text = "Starting Game In: " + Mathf.RoundToInt(time).ToString();
   //
   //             yield return new WaitForSeconds(1f);
   //         }
   //         else
   //         {
   //             PlayerActivation.instance.ContinueToGame();
   //             countingDown = false;
   //             break;
   //         }
   //     }
   // }

    // Update is called once per frame
    void Update () {
		
	}
}
