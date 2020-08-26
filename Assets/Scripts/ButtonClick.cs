using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonClick : MonoBehaviour, ISelectHandler
{
    public AudioSource buttonClick;

    public void OnSelect(BaseEventData eventData)
    {
        buttonClick.Play();
    }
}
