using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound {


    public string name;
    public AudioClip clip;
    
    [Header("Sound Settings")]
    [Range(0,1)]
    [Tooltip("This controls the sounds' volume")]
    public float volume;
    [Range(0,1)]
    [Tooltip("This controls the sounds' pitch")]
    public float pitch = 1;
    [Range(-1,1)]
    [Tooltip("This controls the sounds' pan from left to right(negative to positive)")]
    public float pan;
    [Tooltip("Should the sound loop?")]
    public bool loop;
    [Tooltip("Should the sound play from the start of the level?")]
    public bool onAwake;
    [HideInInspector]
    public AudioSource source;

}
