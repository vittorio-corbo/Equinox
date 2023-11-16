using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverRight : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Lever>() != null)
        {
            other.GetComponent<Lever>().ChangeState(Lever.STATE.Right);
        }
    }
}
