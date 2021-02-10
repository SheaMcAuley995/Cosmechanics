using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Callbacks;
using UnityEditor;

namespace Dialogue.Editor
{
    public class DialogueEditor : EditorWindow
    { 
        //[MenuItem("Window/Dialogue Editor")]
        //public static void ShowEditorWindow()
        //{
        //    GetWindow(typeof(DialogueEditor), false, "Dialogue Editor");
        //}
        //
        //[OnOpenAssetAttribute(1)]
        //public static bool OnOpenAsset(int instanceID, int line)
        //{
        //    Dialogue dialogue = EditorUtility.InstanceIDToObject(instanceID) as Dialogue;
        //    if(dialogue != null)
        //    {
        //        ShowEditorWindow();
        //        return true;
        //    }
        //    return false;
        //}
        //
        //private void OnGUI()
        //{
        //    EditorGUI.LabelField(new Rect(10, 10, 200, 200), "Hello world");
        //    Debug.Log("onGUI");
        //    Repaint(); 
        //}
    }
}

