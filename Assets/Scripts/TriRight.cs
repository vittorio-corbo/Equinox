using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriRight : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Lever>() != null)
        {   
            other.GetComponent<Lever>().ChangeState(Lever.STATE.Right);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Lever>() != null)
        {
            other.GetComponent<Lever>().ChangeState(Lever.STATE.Off);
        }
    }
}
