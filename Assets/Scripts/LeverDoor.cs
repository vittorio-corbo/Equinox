using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverDoor : Lever
{
    public DoorStates door;
    Reporter rep;
    bool doFix;
    // Start is called before the first frame update
    void Start()
    {
        rep = GetComponent<Reporter>();
    }

    public override void ChangeState(STATE newState)
    {
        base.ChangeState(newState);
        doFix = true;
    }
    public override void LeftRoutine()
    {
        if (door.currentState != DoorStates.DoorState.OPEN)
        {
            door.ChangeState(DoorStates.DoorState.OPEN);
        }
    }

    public override void RightRoutine()
    {
        if (door.currentState != DoorStates.DoorState.CLOSED)
        {
            door.ChangeState(DoorStates.DoorState.CLOSED);
        }
    }
}
