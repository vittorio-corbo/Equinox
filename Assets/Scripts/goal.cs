using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //speed = speed * -1;
        //SceneManager.LoadScene("SampleScene 2", LoadSceneMode.Additive);

        //get number of scenes from scene managerrr (or make credits scenes)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneManager.LoadScene("SampleScene 2");

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;


    }
}

