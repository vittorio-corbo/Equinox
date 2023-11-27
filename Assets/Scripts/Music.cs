using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System;

public class Music : MonoBehaviour
{
    /**
     * This script will be used to ensure that the correct music track is playing for a given situation
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
    public AudioMixer mix;
    private AudioSource audio;
    public bool BeatzAreDroppin = true;
    private bool stopped = false;

    private bool paused;
    public AudioClip clipToUse;

    private string exparam = "masterVol";

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        if (BeatzAreDroppin)
        {
            volume = MenuActions.musicVolume;
            StartMusic(clipToUse);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!BeatzAreDroppin && !stopped)
        {
            StopMusic();
            stopped = true;
        }
        mix.SetFloat("settingsVol", Mathf.Log10(MenuActions.musicVolume) * 20);
        if (!paused && PauseScript.isPaused || paused && !(PauseScript.isPaused)) {
            pause();
        }
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

    void pause()
    {
        if (BeatzAreDroppin && !(PauseScript.isPaused))
        {
            audio.UnPause();
        }
        else
        {
            audio.Pause();
        }
        paused = !paused;
    }

    void StartMusic(AudioClip clip)
    {
        audio.clip = clip;
        audio.loop = true;
        audio.Play();
        StartCoroutine(StartFade(audio, mix, exparam, 3f, 1.0f, false));
    }

    //Test both this and StartMusic in a scene
    void StopMusic()
    {
        StartCoroutine(StartFade(audio, mix, exparam, 3f, 0f, true));
    }


    public static IEnumerator StartFade(AudioSource src, AudioMixer audioMixer, string exposedParam, float duration, float targetVolume, bool stopping)
    {
        float currentTime = 0;
        float currentVol;
        audioMixer.GetFloat(exposedParam, out currentVol);
        currentVol = Mathf.Pow(10, currentVol / 20);
        float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
            audioMixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
            yield return null;
        }

        if (stopping)
        {
            src.Stop();
        }
        yield break;
    }
}
