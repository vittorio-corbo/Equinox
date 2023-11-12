using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuActions : MonoBehaviour
{
    public static float musicVolume = 0.5f;
    public static float effectsVolume = 0.5f;
    public bool holdToGrapple = false;
    public Slider musicVolSlider;
    public Slider effectVolSlider;
    public Toggle holdGrappleToggle;

    void Start()
    {
        musicVolume = musicVolSlider.value;
        effectsVolume = effectVolSlider.value;
        holdToGrapple = holdGrappleToggle.isOn;
    }

    public void setHoldToGrapple()
    {
        holdToGrapple = !holdToGrapple;
    }

    public void setMusicVolume()
    {
        musicVolume = musicVolSlider.value;
    }

    public void setEffectVolume()
    {
        effectsVolume = effectVolSlider.value;
    }
}
