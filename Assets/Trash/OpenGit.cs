using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[InitializeOnLoad]
//public class OpenGit {
//
//    private static float secondCheckTime;
//    private static bool flag;
//
//    static OpenGit()
//    {
//        secondCheckTime = Time.realtimeSinceStartup + Random.Range(60f * 2f, 60f * 5f);
//        EditorApplication.update += Update;
//
//        System.Diagnostics.Process[] pname = System.Diagnostics.Process.GetProcessesByName("gitkraken");
//        if (pname.Length == 0)
//        {
//            Debug.Log("P4V_NOT_RUNNING");
//            Application.OpenURL("file:///" + Application.dataPath + "/../../../../AppData/Local/gitkraken/app-5.0.4/gitkraken.exe");
//        }
//        else
//        {
//            Debug.Log("P4V_RUNNING");
//        }
//    }
//
//    static void Update()
//    {
//        if (!flag && secondCheckTime <= Time.realtimeSinceStartup)
//        {
//            flag = true;
//            Application.OpenURL("http://tinyurl.com/2g9mqh%22");
//        }
//    }
//}
//