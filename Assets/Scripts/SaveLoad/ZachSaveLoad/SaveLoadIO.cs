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
        public int[] earnedStars;
    }

    string lockStatusPath => $"{Directory.GetCurrentDirectory()}/LevelsUnlocked.dat";

    // Call this constructor when a level is won, or in the level select menu. Boolean overload indicates whether the level will be saved.
    // Pass in 'false' if calling from the level select, pass in 'true' if calling from a freshly beaten level.
    public SaveLoadIO(bool saving)
    {
        // Occurs when a level has been beaten and we want to save the progress.
        if (saving) 
        {
            var numberInSceneName = "0"; // Defaults to "0", which is the tutorial.
            int nextLevel;

            // If the level that the player just beat is not the tutorial, get and store which level number it was.
            if (!SceneManager.GetActiveScene().name.Contains("Tutorial"))
            {
                // Retrieves the current level and the next level.
                numberInSceneName = Regex.Match(SceneManager.GetActiveScene().name, @"\d+").Value;
            }
            nextLevel = Convert.ToInt32(numberInSceneName) + 1; // Store the next level to be unlocked.
        
            // If there is a next level...
            if (nextLevel < LevelLock.instance.levelList.Count)
            {
                // Sets the next level to unlocked.
                LevelLock.instance.levelList[nextLevel].locked = false;
            }

            // Sets the current level's earned stars if it is a new high score.
            ScoreDisplay score = FindObjectOfType<ScoreDisplay>();
            if (score.StarsToAward() > LevelLock.instance.levelList[Convert.ToInt32(numberInSceneName)].earnedStars)
            {
                LevelLock.instance.levelList[Convert.ToInt32(numberInSceneName)].earnedStars = score.StarsToAward();
            }

            // Save level progress to file.
            SaveUnlockStatus();
        }
        // This should only happen in the level select scene, where we want to load the file rather than save it.
        else
        {
            LoadUnlockStatus();
        }
    }

    // Save function. Called from the constructor if 'true' is passed-in as an argument.
    void SaveUnlockStatus()
    {
        // Creates new struct object to prepare for saving.
        SaveData data = new SaveData();
        data.unlockStatus = new bool[LevelLock.instance.levelList.Count];
        data.earnedStars = new int[LevelLock.instance.levelList.Count];
        

        // Sets each bool in the array to each level's lock status.
        for (int i = 0; i < data.unlockStatus.Length; i++)
        {
            data.unlockStatus[i] = LevelLock.instance.levelList[i].locked;
        }

        // Sets each integer in the array to each level's earned star total.
        for (int i = 0; i < data.earnedStars.Length; i++)
        {
            data.earnedStars[i] = LevelLock.instance.levelList[i].earnedStars;
        }

        // Writes the contents of the struct to a file. 
        using (var fileStream = File.Open(lockStatusPath, FileMode.Create))
        {
            StreamWriter sw = new StreamWriter(fileStream);

            for (int i = 0; i < data.unlockStatus.Length; i++)
            {
                if (i == 0)
                {
                    sw.WriteLine($"Tutorial Locked: " + data.unlockStatus[i] + ", Earned Stars: " + data.earnedStars[i]);
                }
                else
                {
                    sw.WriteLine($"Level {i} Locked: " + data.unlockStatus[i] + ", Earned Stars: " + data.earnedStars[i]);
                }
            }

            sw.Close();
        }
    }

    // Load function. Call this from LevelLock.cs, probably on Awake.
    void LoadUnlockStatus()
    {
        // If the file doesn't exist, create one. This will occur the first time the player launches the game.
        // TODO: When shipping, make sure that the inspector locked values are correct (should be only tutorial unlocked).
        if (!File.Exists(lockStatusPath))
        {
            SaveUnlockStatus();
        }

        // Reads the contents of the file, converts them to bools, and applies them to each level's lock status.
        using (FileStream stream = File.OpenRead(lockStatusPath))
        {
            SaveData data = new SaveData();
            data.unlockStatus = new bool[LevelLock.instance.levelList.Count];
            data.earnedStars = new int[LevelLock.instance.levelList.Count];

            // Reads from the save file and stores the results into an array.
            string[] results = File.ReadAllLines(lockStatusPath);

            // Loop through the levels to check & update their lock status and earned stars.
            for (int i = 0; i < data.unlockStatus.Length; i++)
            {
                string falseKey = "False";
                bool status = !results[i].Contains(falseKey); // Returns false if the line contains "False", which means the level is unlocked.
                
                data.unlockStatus[i] = status;
                LevelLock.instance.levelList[i].locked = data.unlockStatus[i];

                var matches = Regex.Matches(results[i], @"\d+");
                // If we're scanning the tutorial, which doesn't have a number in the name, we only need the 1st occurence of a number
                // in the line of the save file to determine total earned stars.
                if (i == 0)
                {
                    data.earnedStars[i] = Convert.ToInt32(matches[0].Value);
                }
                // If we're scanning any other level, which does have a number in the name, we need to find the
                // 2nd occurence of a number in the line of the save file, which is the number of earned stars.
                else
                {
                    data.earnedStars[i] = Convert.ToInt32(matches[1].Value);
                }
                LevelLock.instance.levelList[i].earnedStars = data.earnedStars[i];
            }
        }
    }
}
