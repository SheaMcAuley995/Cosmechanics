using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataService : MonoBehaviour
{
    private static DataService inst = null;
    public static DataService Instance
    {
        get
        {
            if(inst == null)
            {
                inst = FindObjectOfType<DataService>();

                if(inst == null)
                {
                    GameObject gameObject = new GameObject(typeof(DataService).ToString());
                    inst = gameObject.AddComponent<DataService>();
                }
            }

            return inst;
        }
    }

    public PlayerSettingsSave prefs { get; private set; }


    private void Awake()
    {
        if(Instance != this)
        {
            Destroy(this);
        }
        else
        {
            DontDestroyOnLoad(gameObject);

            prefs = new PlayerSettingsSave();
            prefs.RestorePrefrences();
        }
    }
}
