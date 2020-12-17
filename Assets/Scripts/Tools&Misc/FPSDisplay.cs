using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// FPS counter with customizable color, positioning, size, and automatic text-scaling.
public class FPSDisplay : MonoBehaviour
{
    [Header("Text Settings")]
    [SerializeField] TextAnchor textPosition;
    enum Size { Small, Medium, Large };
    [SerializeField] Size fontSize;
    [SerializeField] Color fontColor = new Color(0.0f, 255.0f, 0.0f, 1.0f); // Default color: green

    float deltaTime = 0.0f;


    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h/* * 2 / 100*/);
        style.alignment = textPosition;

        switch (fontSize)
        {
            case Size.Small:
                style.fontSize = h * 2 / 100;
                break;
            case Size.Medium:
                style.fontSize = h * 3 / 100;
                break;
            case Size.Large:
                style.fontSize = h * 4 / 100;
                break;
            default:
                goto case Size.Small;
        }

        style.normal.textColor = fontColor;

        float miliSeconds = deltaTime * 1000.0f;
        float framesPerSecond = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", miliSeconds, framesPerSecond);
        GUI.Label(rect, text, style);
    }
}
