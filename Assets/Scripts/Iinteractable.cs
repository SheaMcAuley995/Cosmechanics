using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For interacting with objects 
// e.g. plugging in batteries
// or reinserting pipes
public interface IInteractable
{
    void InteractWith();
}

// For picking up objects
public interface IPickUpable
{
    void PickUp();
    void DropObject();
}

// For dropping objects
public interface IDropable
{
    void DropObject();
}

// For applying status effects to objects
public interface IStatusEffect
{
    void Wet();
    void Electricity();
    void Fire();
    void Florp();
    void Blunt();
}
