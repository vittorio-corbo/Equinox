using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnKinematic : MonoBehaviour
{
    [SerializeField] GameObject Liver;

    public void MakeKinematic() { //stop its movement
        Liver.GetComponent<Rigidbody>().isKinematic = true;
        Liver.GetComponent<Rigidbody>().tag = "Untagged";
        //maybe check if grapple is on it, then do it?
        //maybe only activate IFFFFFFF the player shoots at the object

    }
    public void StopKinematic() {
        Liver.GetComponent<Rigidbody>().isKinematic = false;
        Liver.GetComponent<Rigidbody>().tag = "MoveableObject";
    }
}
