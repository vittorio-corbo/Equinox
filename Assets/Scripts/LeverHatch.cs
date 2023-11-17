using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverHatch : Lever
{
    public HatchStates door;
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
        if (door.currentState != HatchStates.HatchState.OPEN)
        {
            door.ChangeState(HatchStates.HatchState.OPEN);
        }
    }

    public override void RightRoutine()
    {
        if (door.currentState != HatchStates.HatchState.CLOSED)
        {
            door.ChangeState(HatchStates.HatchState.CLOSED);
        }
    }
}
