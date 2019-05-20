using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue {

    [Header("Add to and fill these out in order of appearance")]
    public string name;
    [TextArea(3, 10)]
    public string[] sentences;
}
