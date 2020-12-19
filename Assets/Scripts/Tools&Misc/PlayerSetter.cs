//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerSetter : MonoBehaviour
//{
//    PlayerController[] players;
//    CameraMultiTarget example;


//    void Start ()
//    {
//        players = FindObjectsOfType<PlayerController>();
//        example = FindObjectOfType<CameraMultiTarget>();
//        var targets = new List<GameObject>(players.Length);
    
//        CharacterHandler.instance.SetSpawnPoints();
    
//        for (int i = 0; i < players.Length; i++)
//        {
//            players[i].enabled = true;
    
//            if (players[i].cameraTrans == null)
//            {
//                targets.Add(players[i].gameObject);
//                players[i].cameraTrans = Camera.main.transform;
//            }
//            players[i].gameObject.transform.position = CharacterHandler.instance.spawnPoints[i];
//        }
    
//        example.SetTargets(targets.ToArray());
//	}
//}
