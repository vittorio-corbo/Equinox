using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnKinematic : MonoBehaviour
{
    [SerializeField] GameObject Liver;

    public void MakeKinematic() {
        Liver.GetComponent<Rigidbody>().isKinematic = true;
        Liver.GetComponent<Rigidbody>().tag = "Untagged";
        

    }
    public void StopKinematic() {
        Liver.GetComponent<Rigidbody>().isKinematic = false;
        Liver.GetComponent<Rigidbody>().tag = "MoveableObject";

    }
}
