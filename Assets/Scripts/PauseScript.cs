using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public GameObject options;
    public static bool isPaused { get; set; }
    public GameObject pauseMenu;
    public bool canPause = true;
    // Start is called before the first frame update
    void Start()
    {
        canPause = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canPause)
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pause()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void ResetCheckpoint()
    {
        Resume();
        Debug.Log("loading");
        foreach (SaveAndLoad go in Resources.FindObjectsOfTypeAll(typeof(SaveAndLoad)))
        {
            go.Load();
        }
    }

    public void Options()
    {
        options.SetActive(true);
        pauseMenu.SetActive(false);
        canPause = false;
    }
    
    public void CloseOptions()
    {
        canPause = true;
        options.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void QuitGame()
    {
        //Comment this out when we have a start scene
        Application.Quit();
        //SceneManager.LoadScene(0);
    }

    public static void pauseHelper(bool isPaused)
    {
        if (isPaused)
        {

        }
        else
        {

        }
    }
}
