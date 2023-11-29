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
     * Other Contributors: the Google search engine, Chase (in spirit)
     * Bugs found/fixed: idk at this point
     * Heads banged on wall: 10
     */

    //////////////////////DATA/////////////////////////
    string sceneName;

    //TODO: add a volume setting for music. The setting should change this prefab, so check for changes to that
    //Setting in the Update() method pls.
    public float volume;
    public float doppler;
    public AudioMixer mix;
    public AudioSource audio;
    public bool BeatzAreDroppin = true;
    private bool stopped = false;
    public bool fading = false;
    public AudioClip jingle;

    //Use this when you need audio to play that an AreaTrigger should not interfere with
    //NOTE: after setting this you should stop whatever music is playing
    public bool areaTriggerOverride;

    private bool paused;
    public AudioClip clipToUse;
    public bool isPlaying = false;

    private string exparam = "masterVol";

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        mix.SetFloat("settingsVol", Mathf.Log10(PlayerPrefs.GetFloat("MusicVol")) * 20);
        if (!paused && PauseScript.isPaused || paused && !(PauseScript.isPaused)) {
            pause();
        }
    }

    //Initiates a new music clip
    //DEPRECATED
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

    public void StartMusic(AudioClip clip)
    {
        audio.clip = clip;
        audio.loop = true;
        audio.Play();
        StartCoroutine(StartFade(audio, mix, exparam, 3f, 1.0f, false));
        isPlaying = true;
    }

    //Test both this and StartMusic in a scene
    public void StopMusic()
    {
        StartCoroutine(StartFade(audio, mix, exparam, 3f, 0f, true));
    }

    public void PlayOnce(AudioClip clip)
    {
        StartCoroutine(PlayOnceHelper(clip));
    }
    public IEnumerator PlayOnceHelper(AudioClip clip)
    {
        this.isPlaying = true;
        audio.clip = clip;
        audio.loop = false;
        mix.SetFloat(exparam, Mathf.Log10(1.0f) * 20);
        audio.Play();
        yield return new WaitUntil(() => audio.isPlaying == false);
        audio.Stop();
        mix.SetFloat(exparam, Mathf.Log10(0.0f) * 20);
        this.isPlaying = false;
        yield return null;
    }


    public IEnumerator StartFade(AudioSource src, AudioMixer audioMixer, string exposedParam, float duration, float targetVolume, bool stopping)
    {
        fading = true;
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
            isPlaying = false;
        }
        fading = false;
        yield break;
    }
}
