using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void InteractWith();
}

public interface IPickUpable
{
    void Pickup();
}

public interface IStatusEffect
{
    void Wet();
    void Electricity();
    void Fire();
    void Florp();
}
