using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Music : MonoBehaviour
{
    /**
     * This script will be applied to the Music Generator GameObject.
     * The purpose of the script is to read the current scene name,
     * set the music accordingly, and begin playing the music on a loop.
     * This allows us to have a prefab of the music generator while having 
     * different music tracks depending on the scene.
     * 
     * This script was originally created by James Vogt
     * Other Contributors:
     * Bugs found/fixed: 1
     * Heads banged on wall: 0 (for now)
     */

    //////////////////////DATA/////////////////////////
    string sceneName;

    //TODO: add a volume setting for music. The setting should change this prefab, so check for changes to that
    //Setting in the Update() method pls.
    public float volume;
    public float doppler;
    AudioSource audio;
    public bool BeatzAreDroppin = true;


    public AudioClip clipToUse;

    // Start is called before the first frame update
    void Start()
    {
        if (BeatzAreDroppin)
        {
            audio = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
            sceneName = SceneManager.GetActiveScene().name;
            BeatDrop(audio, clipToUse, volume, doppler);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Initiates a new music clip
    bool BeatDrop(AudioSource music, AudioClip beatz, float volume, float doppler)
    {
        bool success; 
        try
        {
            music.loop = true;
            music.volume = volume;
            music.clip = beatz;
            music.dopplerLevel = doppler;
            music.Play();
            success = true;
            return success;
            
        } catch (Exception e)
        {
            success = false;
            Debug.LogException(e);
            return success;
        }
    }
}
