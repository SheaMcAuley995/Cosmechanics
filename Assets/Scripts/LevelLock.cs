using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLock : MonoBehaviour
{
    public RawImage lockImage;
    public bool locked;

    Button button;

    private void Start()
    {
        button = GetComponent<Button>();

        lockImage.enabled = locked;

        if(locked == true)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }
}
