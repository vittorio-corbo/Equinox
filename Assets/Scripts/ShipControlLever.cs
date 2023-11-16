using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControlLever : Lever
{
    [SerializeField] private GameObject MovingGameObject;
    [SerializeField] private Vector3 RotationVector;
    public override void LeftRoutine() {
        MovingGameObject.transform.Rotate(-RotationVector);
    }

    public override void RightRoutine() {
        MovingGameObject.transform.Rotate(RotationVector);
    }
}
