using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GrabbableDisenable : MonoBehaviour
{
    private bool isSticked = false;

    public void Sticks(Transform target)
    {
        // Disable the rigidbody and collider to prevent further physics interactions.
        Rigidbody rb = GetComponent<Rigidbody>();
        Collider col = GetComponent<Collider>();
        rb.isKinematic = true;
        col.enabled = false;

        // Attach the object to the target GameObject.
        transform.parent = target;
        isSticked = true;
    }

    public void Release()
    {
        // If player wants to pick up grabbable object, restore the object's original state.
        Rigidbody rb = GetComponent<Rigidbody>();
        Collider col = GetComponent<Collider>();
        rb.isKinematic = false;
        col.enabled = true;
        transform.parent = null;
        isSticked = false;
    }
}
