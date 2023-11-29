using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicReportee : Reportee
{
    /**
     * This class represents a reportee that is to be attached to an AreaTrigger to
     * determine what music is meant to play in a particular area.
     * The minions field should contain all Reporters that report to this class,
     * and they should be ***in the same order as the music clips you want to play***.
     * The music clips need to have a String attached to them (such as "010110")
     * that corresponds to the reporters that should be fixed or not fixed, 
     * ***in the order that they should be checked***. 
     */
    public AudioClip[] clipsToPlay;
    public string[] keys;
    public Dictionary<string, AudioClip> clips;
    private string clipKey;
    private string prevClipKey;
    public Music musicScript;
    private AudioClip lastClipPlayed = null;
    [SerializeField]
    private AreaTrigger trigger;
    // Start is called before the first frame update
    public override void Start()
    {
        trigger = GetComponent<AreaTrigger>();
        clips = new Dictionary<string, AudioClip> ();
        base.Start();
        if (clipsToPlay.Length == keys.Length)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                clips.Add(keys[i], clipsToPlay[i]);
            }
        }
        else
        {
            Debug.LogWarning("A key in MusicReportee is missing its audio clip, or vice versa");
        }
        prevClipKey = ReportHelper();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void GatherReport()
    {
        Debug.Log("GatherReport called");
        clipKey = ReportHelper();
        Debug.Log(clipKey);
        if (trigger.inArea && musicScript.areaTriggerOverride == false)
        {
            if (musicScript.isPlaying)
            {
                if (prevClipKey != clipKey)
                {
                    StartCoroutine(WaitForFadeThenSwitch(clips[clipKey], true));
                }
                else
                {
                    StartCoroutine(WaitForFadeThenSwitch(clips[clipKey], false));
                }
            }
            else
            {
                musicScript.StartMusic(clips[clipKey]);
            }
        }
        else if (musicScript.isPlaying && clipKey != prevClipKey && musicScript.areaTriggerOverride == false)
        {
            AudioClip temp = musicScript.audio.clip;
            StartCoroutine(WaitForFadeThenSwitch(temp, true));
        }
        else if (clipKey != prevClipKey && musicScript.areaTriggerOverride == false)
        {
            musicScript.PlayOnce(musicScript.jingle);
        }
        prevClipKey = clipKey;
    }

    private string ReportHelper()
    {
        Debug.Log("In ReportHelper");
        string a = "";
        foreach (Reporter rep in minions)
        {
            if (rep.GetFixed() == true)
            {
                a += "1";
            }
            else
            {
                a += "0";
            }
        }
        return a;
    }

    //Switches tracks to the next one by running a fadeout in Music then a fadein when the fadeout is done
    public IEnumerator SwitchTracks(AudioClip newTrack, bool jingle)
    {
        Debug.Log("Jingle: " + jingle);
        if (musicScript.isPlaying)
        {
            musicScript.StopMusic();
        }
        yield return new WaitUntil(() => !musicScript.fading);
        if (jingle)
        {
            musicScript.PlayOnce(musicScript.jingle);
        }
        yield return new WaitUntil(() => !musicScript.isPlaying);
        musicScript.StartMusic(newTrack);
        yield return null;
    }

    //Resolves a bug caused by players fixing things too quickly
    public IEnumerator WaitForFadeThenSwitch(AudioClip clip, bool jingle)
    {
        yield return new WaitUntil(() => !musicScript.fading);
        StartCoroutine(SwitchTracks(clip, jingle));
    }
}
