using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOutTrash : MonoBehaviour
{
    [SerializeField] PlayerController[] oldCharacters;
    [SerializeField] PickUp[] oldPickups;

    void Awake()
    {
        oldPickups = FindObjectsOfType<PickUp>();
        for (int i = 0; i < oldPickups.Length; i++)
        {
            Destroy(oldPickups[i].gameObject);
        }

        oldCharacters = FindObjectsOfType<PlayerController>();

        if (oldCharacters.Length > 0)
        {
            foreach (PlayerController player in oldCharacters)
            {
                Destroy(player.gameObject);
            }
        }
    }
}
