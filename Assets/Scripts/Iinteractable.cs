using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void InteractWith();
}

public interface IPickUpable
{
    void PickUp();
}

public interface IStatusEffect
{
    void Wet();
    void Electricity();
    void Fire();
    void Florp();
    void Blunt();
}
