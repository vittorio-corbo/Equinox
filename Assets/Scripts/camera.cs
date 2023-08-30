using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour{
    // Start is called before the first frame update
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update() {
        //X AXIS
        if (Input.GetAxis("Mouse X") < 0) {
            //Code for action on mouse moving left
            print("Mouse moved left");
            //transform.Rotate(-1.0f, 0.0f, 0.0f, Space.Self);
            //transform.Rotate(0.0f, -0.1f, 0.0f, Space.Self) * Time.deltaTime;
            transform.Rotate(0.0f, -0.5f, 0.0f, Space.Self) ;
        }
        if (Input.GetAxis("Mouse X") > 0) {
            //Code for action on mouse moving right
            print("Mouse moved right");
            //transform.Rotate(1.0f, 0.0f, 0.0f, Space.Self);
            transform.Rotate(0.0f, 0.5f, 0.0f, Space.Self);
        }

        //Y AXIS
        if (Input.GetAxis("Mouse Y") < 0) {
            //Code for action on mouse moving left
            print("Mouse moved left");
            transform.Rotate(0.5f, 0.0f, 0.0f, Space.Self);
            //transform.Rotate(0.0f, -1.0f, 0.0f, Space.Self);
        }
        if (Input.GetAxis("Mouse Y") > 0){
            //Code for action on mouse moving right
            print("Mouse moved right");
            transform.Rotate(-0.5f, 0.0f, 0.0f, Space.Self);
            //transform.Rotate(0.0f, 1.0f, 0.0f, Space.Self);
        }


        //QUIT GAME

        if (Input.GetKey("escape")){
            Application.Quit();
        }

    }
}

