using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    // If you want the narrator to have a personality ¯\_(ツ)_/¯
    //public string name;

    [Header("Add to and fill these out in order of appearance")]
    public string name;
    [TextArea(3, 10)]
    public string[] sentences;
}
