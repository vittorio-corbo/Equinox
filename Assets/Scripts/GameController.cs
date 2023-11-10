using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public Image controlImage;

    void Start()
    {
        controlImage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!(PauseScript.isPaused))
        {
            //Quit Game
            if (Input.GetKey("escape"))
            {
                Application.Quit();
            }
            //RESET ROOM
            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                controlImage.gameObject.SetActive(!controlImage.gameObject.activeSelf);
            }
        }

    }
}
