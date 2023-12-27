using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject options;

    public GameObject credits;
    public GameObject main;
    public string StartButtonScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame() {
        SceneManager.LoadScene(1);
        PauseScript.isPaused = false;
        PauseScript.pauseHelper(PauseScript.isPaused);
    }
    //public void OpenOptions() { }
    //public void CloseOptions() { }
    public void QuitGame() { 
        Application.Quit();
    }

    public void openOptions()
    {
        options.SetActive(true);
        main.SetActive(false);
    }

    public void closeOptions()
    {
        options.SetActive(false);
        main.SetActive(true);
    }

    public void openCredits()
    {
        credits.SetActive(true);
        main.SetActive(false);
    }

    public void closeCredits()
    {
        credits.SetActive(false);
        main.SetActive(true);
    }
}
