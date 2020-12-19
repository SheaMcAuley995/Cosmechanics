using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(Text))] /// For if using the Unity Standard Assets version
public class FPSDisplay : MonoBehaviour
{
    #region Unity Standard Assets Version (I don't like it as much)
    //const float fpsMeasurePeriod = 0.5f;
    //private int m_FpsAccumulator = 0;
    //private float m_FpsNextPeriod = 0;
    //private int m_CurrentFps;
    //const string display = "{0} FPS";
    //private Text m_Text;


    //void Start()
    //{
    //    m_FpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;
    //    m_Text = GetComponent<Text>();
    //}


    //void Update()
    //{
    //    // measure average frames per second
    //    m_FpsAccumulator++;
    //    if (Time.realtimeSinceStartup > m_FpsNextPeriod)
    //    {
    //        m_CurrentFps = (int)(m_FpsAccumulator / fpsMeasurePeriod);
    //        m_FpsAccumulator = 0;
    //        m_FpsNextPeriod += fpsMeasurePeriod;
    //        m_Text.text = string.Format(display, m_CurrentFps);
    //    }
    //}
    #endregion

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
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);

        float miliSeconds = deltaTime * 1000.0f;
        float framesPerSecond = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", miliSeconds, framesPerSecond);
        GUI.Label(rect, text, style);
    }
    #endregion
}
