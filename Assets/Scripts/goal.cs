using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goal : MonoBehaviour
{
    public void NextLevel()
    {

        //get number of scenes from scene manager (or make credits scenes)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}

