using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectDisplayScript : MonoBehaviour
{
    RawImage displayImage;
    
    private void Start()
    {
        displayImage = GetComponent<RawImage>();        
    }

    public void SwapImage(Texture image)
    {
        displayImage.texture = image;
    }
}
