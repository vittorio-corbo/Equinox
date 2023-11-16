using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriStateRight : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TriStateLever>() != null)
        {
            other.GetComponent<TriStateLever>().ChangeState(TriStateLever.STATE.Right);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<TriStateLever>() != null)
        {
            other.GetComponent<TriStateLever>().ChangeState(TriStateLever.STATE.Off);
        }
    }
}
