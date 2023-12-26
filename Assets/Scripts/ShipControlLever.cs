using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControlLever : Lever
{
    [SerializeField] private GameObject MovingGameObject;
    [SerializeField] private Vector3 RotationVector;
    [SerializeField] private Reportee reportee;


    public override void LeftRoutine() {
        //if (reportee != null){}
        if (reportee.allFixed){
            MovingGameObject.transform.Rotate(-RotationVector);
            //light up too
        }
    }

    public override void RightRoutine() {
        if (reportee.allFixed){
            MovingGameObject.transform.Rotate(RotationVector);
            //light up too
        }
    }
}
