using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerCameraRotateController : MonoBehaviour
{

    [SerializeField] private Transform player;
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

        InSpaceRotationCalculation(mouseX, mouseY);

    }

    private void InSpaceRotationCalculation(float mouseX, float mouseY)
    {
        float cameraPitchRotation = mouseY * mouseSensitivity;
        if (Input.GetMouseButton(1))
        {
            Vector3 cameraForward = transform.forward;
            //if right button pressed, then mouseX does rotation around the roll axis
            player.Rotate(cameraForward, mouseX * mouseSensitivity, Space.World);
                          //roll axis,   angle,   WorldRotation insteaed of local
        }
        else
        {
            float cameraYawRotation = mouseX * mouseSensitivity;
            //along the up vector
            player.Rotate(cameraYawRotation * Vector3.up);
        }
        //along the horizontal vector, independent of mouse button press
        player.Rotate(-cameraPitchRotation * Vector3.right);
    }
}
