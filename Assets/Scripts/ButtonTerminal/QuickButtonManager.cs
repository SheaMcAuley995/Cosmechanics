using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickButtonManager : MonoBehaviour
{
    public enum InputEvent
    {
        Input0,
        Input1,
        Input2,
        Input3,
        Input4,
        Input5
    }
    public InputEvent inputEvent = InputEvent.Input0;

    public ButtonCanvasView canvasView;
    [Space]
    public List<Image> images = new List<Image>();
    [Space]
    public List<Sprite> buttonSprites = new List<Sprite>();
    [Space]
    public List<string> inputs = new List<string>();

    string playerTag = "Char";

    PlayerController playerInRange;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            GenerateRandomSequence();
            playerInRange = other.GetComponent<PlayerController>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            canvasView.ToggleImages(false);
            playerInRange = null;
            inputEvent = InputEvent.Input0;
        }
    }

    void GenerateRandomSequence()
    {
        canvasView.ToggleImages(true);

        for (int i = 0; i < images.Count; i++)
        {
            int index = Random.Range(0, buttonSprites.Count);

            images[i].sprite = buttonSprites[index];
            inputs[i] = buttonSprites[index].name;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            switch (inputEvent)
            {
                case InputEvent.Input0:
                    CheckInput(inputs[0]);
                    break;
                case InputEvent.Input1:
                    CheckInput(inputs[1]);
                    break;
                case InputEvent.Input2:
                    CheckInput(inputs[2]);
                    break;
                case InputEvent.Input3:
                    CheckInput(inputs[3]);
                    break;
                case InputEvent.Input4:
                    CheckInput(inputs[4]);
                    break;
                case InputEvent.Input5:
                    CheckInput(inputs[5]);
                    break;
            }
        }
    }

    void CheckInput(string input)
    {
        
    }
}
