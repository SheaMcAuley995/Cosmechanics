using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound {


    public string name;
    public AudioClip clip;
    
    [Header("Sound Settings")]
    [Range(0,1)]
    public float volume;
    [Range(0,1)]
    public float pitch = 1;
    [Range(-1,1)]
    public float pan;
    public bool loop;
    public bool onAwake;
    //[HideInInspector]
    public AudioSource source;

}
