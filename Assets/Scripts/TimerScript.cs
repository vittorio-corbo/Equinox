using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    int hours;
    int minutes;
    int seconds;
    int milliseconds;
    TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        Debug.Log("Timer " + PlayerPrefs.GetInt("Timer"));
        if (!PlayerPrefs.HasKey("Timer") || PlayerPrefs.GetInt("Timer") == 0)
        {
            gameObject.SetActive(false);
            return;
        }
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        hours = 0;
        minutes = 0;
        seconds = 0;
        milliseconds = 0;
        while (true)
        {
            yield return new WaitForSeconds(0.009f);
            milliseconds++;
            if (milliseconds >= 100)
            {
                milliseconds %= 100;
                seconds++;
                if (seconds >= 60)
                {
                    seconds %= 60;
                    minutes++;
                    if (minutes >= 60)
                    {
                        minutes %= 60;
                        hours++;
                    }
                }
            }
            text.text = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", hours, minutes, seconds, milliseconds);
        }
    }
}
