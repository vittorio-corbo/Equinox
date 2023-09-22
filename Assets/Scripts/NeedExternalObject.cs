using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedExternalObject : MonoBehaviour
{
    public GameObject fixedText;
    public Transform slot; // specific slot for grabbable object
    public float detectionRadius = 20f;
    private Boolean isFixed = false;
    private GrabbableDisenable grabbedObject = null;
    // Start is called before the first frame update
    void Start()
    {
        fixedText.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {
        // if grappable object is collided in the correct slot, isFixed is true, and grabbable object is inserted to the needExternalObject
        // if isFixed, hint the player

        if (grabbedObject != null)
        {
            grabbedObject.Sticks(transform);
        }

        // Check if the text is active and hide it after 2 seconds
        if (fixedText.activeSelf)
        {
            StartCoroutine(HideTextAfterDelay(2f));
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Grabbable"))
        { 
            isFixed = true;
            grabbedObject = collision.gameObject.GetComponent<GrabbableDisenable>();
            fixedText.SetActive(true);
        }
    }
    private IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        fixedText.SetActive(false);
    }
}

