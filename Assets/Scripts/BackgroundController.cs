using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] List<Sprite> backgrounds;
    int current;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("TextureNum"))
        {
            PlayerPrefs.SetInt("TextureNum", 0);
        }
        current = PlayerPrefs.GetInt("TextureNum");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("left"))
        {
            if (current == 0)
            {
                current = backgrounds.Count - 1;
            }
            else
            {
                current--;
            }
        }
        else if (Input.GetKeyDown("right"))
        {
            if (current == backgrounds.Count - 1)
            {
                current = 0;
            }
            else
            {
                current++;
            }
        }
        GetComponent<Image>().sprite = backgrounds[current];
        PlayerPrefs.SetInt("TextureNum", current);
    }
}
