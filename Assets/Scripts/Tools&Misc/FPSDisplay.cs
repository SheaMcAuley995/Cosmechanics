﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSDisplay : MonoBehaviour
{
    #region Custom Version (simpler & text auto scales with screen sizes)
    float deltaTime = 0.0f;

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = new Color(255.0f, 215.0f, 0.0f, 1.0f);

        float miliSeconds = deltaTime * 1000.0f;
        float framesPerSecond = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", miliSeconds, framesPerSecond);
        GUI.Label(rect, text, style);
    }
    #endregion
}
