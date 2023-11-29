using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnKinematic : MonoBehaviour
{
    [SerializeField] GameObject Liver;
    [SerializeField] float WaitTimer;

    public void MakeKinematic()
    { //stop its movement
        StartCoroutine("WaitForKinematic");

    }
    public void StopKinematic() {
        Liver.GetComponent<Rigidbody>().isKinematic = false;
        Liver.GetComponent<Rigidbody>().tag = "MoveableObject";
    }

    private IEnumerator WaitForKinematic()
    {
        yield return new WaitForSeconds(WaitTimer);
        Liver.GetComponent<Rigidbody>().isKinematic = true;
        Liver.GetComponent<Rigidbody>().tag = "Untagged";
    }
}
