using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;


public class AudioEventManager : MonoBehaviour {
    public List<Sound> sounds;   
    [HideInInspector]public static AudioEventManager instance;
    [SerializeField] bool addAnotherSound;
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
    public void PlaySound(string name, float volume, float pitch, float pan)
    {
       
        Sound s = Array.Find(sounds.ToArray(), sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.volume = volume;
        s.source.pitch = pitch;
        s.source.panStereo = pan;
        s.source.Play(); 
        
    
    } //play sound fucntion with maximum overolads
    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds.ToArray(), sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.Play();
    } //Simple Play Sound function
    public void PlaySound(string name, float volume)
    {
        Sound s = Array.Find(sounds.ToArray(), sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.volume = volume;
        s.source.Play();
    }
    private void Start()
    {
        PlaySound("Theme");
        PlaySound("Ambient", .8f);        
    }
    

    [ExecuteInEditMode] public void addSound()
    {
        if(addAnotherSound)
        {
            sounds.Add(new Sound());
            addAnotherSound = false;
        }

    }
    
}
