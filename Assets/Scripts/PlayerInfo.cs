using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {

    public Material[] teamMat;
    public List<Color> colors;
    private void Start()
    {
        foreach (var mat in teamMat)
        {
            Color col = mat.color;
            colors.Add(col);
        }
    }
}
