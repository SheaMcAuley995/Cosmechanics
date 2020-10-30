using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script will loop through all other components on the same Game Object
/// inheriting from ISaveable and save their respective data.

/// Each saveable object is assigned an identifier (id), to be used when loading 
/// different values to multiple entities with the same data (ex: level & xp).
/// </summary>
public class SaveableEntity : MonoBehaviour
{
    [SerializeField] string id = string.Empty;

    public string Id => id;

    // Callable on the game object, will generate a random ID for it.
    [ContextMenu("Generate ID")]
    void GenerateID() => id = Guid.NewGuid().ToString();

    #region Open me for a fun fact!
    // Fun fact: If you created 103 trillion v4 GUIDs, the chance of getting a
    // duplicate is only 0.0000001‬%.
    // Source: https://en.wikipedia.org/wiki/Universally_unique_identifier#Random_UUID_probability_of_duplicates
    #endregion

    // Loops through all ISaveable components on the game object and stores them in a dictionary.
    public object CaptureState()
    {
        var state = new Dictionary<string, object>();

        foreach (var saveable in GetComponents<ISaveable>())
        {
            state[saveable.GetType().ToString()] = saveable.CaptureState();
            // Ex: key = Object containing savable data, value = SaveData struct containing values.
        }

        return state;
    }

    // Loops through all ISaveable components on the Game Object and converts saved dictionary data to original object.
    public void RestoreState(object state)
    {
        var stateDictionary = (Dictionary<string, object>)state;

        foreach (var saveable in GetComponents<ISaveable>())
        {
            string typeName = saveable.GetType().ToString();

            if (stateDictionary.TryGetValue(typeName, out object value))
            {
                saveable.RestoreState(value);
            }
        }
    }
}
