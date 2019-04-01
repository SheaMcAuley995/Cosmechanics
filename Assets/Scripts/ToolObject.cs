using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ToolObject : MonoBehaviour {


    public virtual void UseObject()
    {
        Debug.Log(name + " is being used");
    }
}
