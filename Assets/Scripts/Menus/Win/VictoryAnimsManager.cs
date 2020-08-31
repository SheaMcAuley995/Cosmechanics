using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryAnimsManager : MonoBehaviour
{
    public Transform[] animPositions = new Transform[4];
    PlayerController[] players;

	// Use this for initialization
	void Start ()
    {
        players = FindObjectsOfType<PlayerController>();

        for (int i = 0; i < players.Length; i++)
        {
            players[i].enabled = false;

            players[i].transform.position = animPositions[i].position;
            players[i].transform.LookAt(Camera.main.transform);
            players[i].transform.Rotate(players[i].transform.rotation.x, players[i].transform.rotation.y, -180f);

            // Gets each player's animator component for the body & head
            Animator bodyAnimator = players[i].GetComponent<Animator>();
            Animator headAnimator = players[i].GetComponentInChildren<Head>().GetComponent<Animator>(); // This is, quite possibly, the most disgusting thing I've ever written lmao

            // Identifies which head each player has in order to determine the correct body animation to play
            switch (headAnimator.gameObject.name)
            {
                case "Rig_Blank_UncleBob(Clone)":
                    bodyAnimator.SetTrigger("UncleBob_Win");
                    break;
                case "Rig_Blank_Fennec(Clone)":
                    bodyAnimator.SetTrigger("Fennec_Win");
                    break;
                case "Rig_Blank_Helmet(Clone)":
                    bodyAnimator.SetTrigger("Helmet_Win");
                    break;
                case "Rig_Blank_Blobfish_04 (1)(Clone)":
                    bodyAnimator.SetTrigger("Blobfish_Win");
                    break;
            }

            // Sets the head animation to play
            headAnimator.SetTrigger("Win");
        }
	}
}
