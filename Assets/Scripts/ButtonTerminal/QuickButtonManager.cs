using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickButtonManager : MonoBehaviour
{
    public ButtonCanvasView canvasView;
    [Space]
    public List<Image> images = new List<Image>();
    [Space]
    public List<Sprite> buttons = new List<Sprite>();

    string playerTag = "Char";


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            GenerateRandomSequence();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            canvasView.ToggleImages(false);
        }
    }

    void GenerateRandomSequence()
    {
        canvasView.ToggleImages(true);

        for (int i = 0; i < images.Count; i++)
        {
            int index = Random.Range(0, buttons.Count);

            images[i].sprite = buttons[index];
        }
    }

    //void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.CompareTag(playerTag))
    //    {

    //    }
    //}
}
