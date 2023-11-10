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
        if (Input.GetKeyDown(KeyCode.P) && canPause)
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
        pauseHelper(isPaused);
    }

    public void Pause()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        pauseHelper(isPaused);
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
        //TODO: Change to build index if needed
        SceneManager.LoadScene("Start");
        SceneManager.UnloadScene("pause");
    }

    public static void pauseHelper(bool isPaused)
    {
        if (isPaused)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
