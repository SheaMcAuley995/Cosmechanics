using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropdownMover : Toggle
{
    public AutomaticScroll autoScroll;

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        autoScroll.OnUpdateSelected(eventData);
    }
}
