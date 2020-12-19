using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadIO : MonoBehaviour
{
    // Struct of data to be saved.
    struct SaveData
    {
        // Lock status of each level.
        public bool[] unlockStatus;
    }

    string savePath => $"{Application.persistentDataPath}/LevelsUnlocked.dat";

    // Call this constructor when a level is won.
    public SaveLoadIO(bool saving)
    {
        if (saving)
        {
            // Retrieves the current level and the next level.
            var numberInSceneName = Regex.Match(SceneManager.GetActiveScene().name, @"\d+").Value;
            int nextLevel = Convert.ToInt32(numberInSceneName) + 1;
        
            // Sets the next level to unlocked.
            LevelLock.instance.levelList[nextLevel].locked = false;
        }
    }

    // Save function. Call this from Engine.cs when the game is won.
    public void SaveUnlockStatus()
    {
        // Creates new struct object to prepare for saving.
        SaveData data = new SaveData();
        data.unlockStatus = new bool[LevelLock.instance.levelList.Count];

        // Sets each bool in the array to each level's lock status.
        for (int i = 0; i < LevelLock.instance.levelList.Count; i++)
        {
            data.unlockStatus[i] = LevelLock.instance.levelList[i].locked;
        }

        // Writes the contents of the struct to a file. 
        using (var fileStream = File.Open(savePath, FileMode.Create))
        {
            StreamWriter sw = new StreamWriter(fileStream);

            for (int i = 0; i < data.unlockStatus.Length; i++)
            {
                if (i == 0)
                {
                    sw.WriteLine($"Tutorial Locked: " + data.unlockStatus[i]);
                }
                else
                {
                    sw.WriteLine($"Level {i} Locked: " + data.unlockStatus[i]);
                }
            }

            sw.Close();
        }
    }

    // Load function. Call this from LevelLock.cs, probably on Awake.
    public void LoadUnlockStatus()
    {
        // If the file doesn't exist, create one. This will occur the first time the player launches the game.
        // TODO: When shipping, make sure that the inspector locked values are correct (should be only tutorial & level 1 unlocked).
        if (!File.Exists(savePath))
        {
            SaveUnlockStatus();
        }

        // Reads the contents of the file, converts them to bools, and applies them to each level's lock status.
        using (FileStream stream = File.OpenRead(savePath))
        {
            SaveData data = new SaveData();
            data.unlockStatus = new bool[LevelLock.instance.levelList.Count];

            // Reads from the save file and stores the results into an array.
            string[] results = File.ReadAllLines(savePath);

            // Loop through the levels to check & update their lock status.
            for (int i = 0; i < data.unlockStatus.Length; i++)
            {
                string falseKey = "False";

                bool status = !results[i].Contains(falseKey); // Returns false if the line contains "False", which means the level is unlocked.
                data.unlockStatus[i] = status;
                LevelLock.instance.levelList[i].locked = data.unlockStatus[i];
            }
        }
    }
}
