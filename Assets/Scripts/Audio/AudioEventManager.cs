using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioEventManager : MonoBehaviour {

    public enum AudioClipType {Music, Effect, Other};

    public List<Sound> sounds;
    [HideInInspector]
    public AudioEventManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);


        foreach (Sound snd in sounds)
        {
            snd.source = gameObject.AddComponent<AudioSource>();
            snd.source.clip = snd.clip;
            snd.source.volume = snd.volume;
            snd.source.pitch = snd.pitch;
            snd.source.panStereo = snd.pan;
            snd.source.playOnAwake = snd.onAwake;
            snd.source.loop = snd.loop;
        }
        
    }
    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds.ToArray(), sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.Play();  
    
    }
    private void Start()
    {
        PlaySound("Theme");
        
        //PlaySound("AlarmRight");
    }
}
