using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlorpButton : MonoBehaviour, IInteractable
{

    public FlorpFiller florpFiller;
    public MeshRenderer meshRenderer;
    public bool On = false;

    public void InteractWith()
    {
        if(On)
        {
            florpFiller.fillFlorp();
        }
    }

}
