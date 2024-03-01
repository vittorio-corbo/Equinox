using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuActions : MonoBehaviour
{
    float musicVolume = 0.5f;
    float effectsVolume = 0.5f;
    bool holdToGrapple = false;
    bool timer = false;

    public Slider musicVolSlider;
    public Slider effectVolSlider;
    public Toggle holdGrappleToggle;
    public Toggle timerToggle;


    void Start()
    {
        if (!PlayerPrefs.HasKey("MusicVol"))
        {
            PlayerPrefs.SetFloat("MusicVol", 1f);
        }
        if (!PlayerPrefs.HasKey("EffectVol"))
        {
            PlayerPrefs.SetFloat("EffectVol", 1f);
        }
        if (!PlayerPrefs.HasKey("HoldGrapple"))
        {
            PlayerPrefs.SetInt("HoldGrapple", 0);
        }
        if (!PlayerPrefs.HasKey("JEANS"))
        {
            PlayerPrefs.SetInt("JEANS", 0);
        }
        if (!PlayerPrefs.HasKey("Timer"))
        {
            PlayerPrefs.SetInt("Timer", 0);
        }
        if (timerToggle != null)
        {
            timerToggle.isOn = PlayerPrefs.GetInt("Timer") == 1;
            timer = timerToggle.isOn;
        }
        musicVolSlider.value = PlayerPrefs.GetFloat("MusicVol");
        effectVolSlider.value = PlayerPrefs.GetFloat("EffectVol");
        holdGrappleToggle.isOn = PlayerPrefs.GetInt("HoldGrapple") == 1;
        musicVolume = musicVolSlider.value;
        effectsVolume = effectVolSlider.value;
        holdToGrapple = holdGrappleToggle.isOn;
    }

    public void setHoldToGrapple()
    {
        holdToGrapple = holdGrappleToggle.isOn;
        PlayerPrefs.SetInt("HoldGrapple", holdToGrapple ? 1 : 0);
    }

    public void setMusicVolume()
    {
        musicVolume = musicVolSlider.value;
        PlayerPrefs.SetFloat("MusicVol", musicVolume);
    }

    public void setEffectVolume()
    {
        effectsVolume = effectVolSlider.value;
        PlayerPrefs.SetFloat("EffectVol", effectsVolume);
    }

    public void SetTimer()
    {
        timer = timerToggle.isOn;
        PlayerPrefs.SetInt("Timer", holdToGrapple ? 1 : 0);
    }
}
