using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerCameraRotateController : MonoBehaviour
{

    [SerializeField] private Transform player;
    private Coroutine cameraMovement;
    public float mouseSensitivity = 1f;

    void Start()
    {
        //lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Debug.Log(player.rotation);
        // Debug.Log(player.rotation.x);

        InSpaceRotationCalculation(mouseX, mouseY);

    }

    private void InSpaceRotationCalculation(float mouseX, float mouseY)
    {

        Vector3 newPos = new Vector3(0, 0, 0);

        float cameraPitchRotation = mouseY * mouseSensitivity;
        if (Input.GetMouseButton(1))
        {
            Vector3 cameraForward = transform.forward;
            //if right button pressed, then mouseX does rotation around the roll axis
            // player.Rotate(cameraForward, -mouseX * mouseSensitivity, Space.World);
                          //roll axis,   angle,   WorldRotation insteaed of local
                          //-1 to make player rotate clockwise when mouse moves right, and anticlockwise for left mouse movmeent

            
            // Forward angle
            newPos.z = -mouseX * mouseSensitivity;
        
        }
        else
        {
            float cameraYawRotation = mouseX * mouseSensitivity;
            //along the up vector
            // player.Rotate(cameraYawRotation * Vector3.up);


            newPos.y = cameraYawRotation;

        }
        //along the horizontal vector, independent of mouse button press
        // player.Rotate(-cameraPitchRotation * Vector3.right);

        newPos.x = -cameraPitchRotation;

        Quaternion rotated = player.rotation * Quaternion.Euler(newPos);
        player.rotation = Quaternion.Slerp(player.rotation, rotated, 1f);
    }
}
