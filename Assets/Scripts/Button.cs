using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    public ButtonScript buttonScript;
    public void BreakOff()
    {
        gameObject.tag = "Grabbable";
        transform.GetChild(0).gameObject.tag = "Grabbable";
        transform.GetChild(0).gameObject.GetComponent<BoxCollider>().isTrigger = false;
        buttonScript = null;
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.freezeRotation = false;
        rb.constraints = RigidbodyConstraints.None;
    }

    public void ReAttach(ButtonScript buttonScript)
    {
        this.buttonScript = buttonScript;
        gameObject.tag = "MoveableObject";
        transform.GetChild(0).gameObject.tag = "MoveableObject";
        transform.GetChild(0).gameObject.GetComponent<BoxCollider>().isTrigger = true;
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    }

    public void OnJointBreak(float breakForce)
    {
        buttonScript.Break();
        BreakOff();
    }
}
