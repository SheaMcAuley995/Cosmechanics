using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// This script contains the File IO logic, and is what you will want to reference when saving/loading data.
/// </summary>
public class SaveScript : MonoBehaviour
{
    public static SaveScript instance;

    void Awake()
    {
        #region Singleton
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        #endregion
    }

    string savePath => $"{Application.persistentDataPath}/save.dat";

    // This is the function you should call when saving. It finds all objects with saveable data, 
    // stores them into a dictionary of objects, and then serializes the data into a file.
    [ContextMenu("Save Data")]
    public void Save()
    {
        var state = LoadFile();
        CaptureState(state);
        SaveFile(state);
    }

    // This is the function you should call when loading. It deserializes the contents of the save file, 
    // stores the data into a dictionary of objects, and then restores the saved values to each saveable
    // entity using their respective GUID.
    [ContextMenu("Load Data")]
    public void Load()
    {
        var state = LoadFile();
        RestoreState(state);
    }

    void SaveFile(object state)
    {
        using (var fileStream = File.Open(savePath, FileMode.Create))
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(fileStream, state);
        }
    }

    private Dictionary<string, object> LoadFile()
    {
        if (!File.Exists(savePath))
        {
            return new Dictionary<string, object>();
        }

        using (FileStream stream = File.Open(savePath, FileMode.Open))
        {
            var formatter = new BinaryFormatter();
            return (Dictionary<string, object>)formatter.Deserialize(stream);
        }
    }

    void CaptureState(Dictionary<string, object> state)
    {
        foreach (var saveable in FindObjectsOfType<SaveableEntity>())
        {
            state[saveable.Id] = saveable.CaptureState();
        }
    }

    void RestoreState(Dictionary<string, object> state)
    {
        foreach (var saveable in FindObjectsOfType<SaveableEntity>())
        {
            if (state.TryGetValue(saveable.Id, out object value))
            {
                saveable.RestoreState(value);
            }
        }
    }
}