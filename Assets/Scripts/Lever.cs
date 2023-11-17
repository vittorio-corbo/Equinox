using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public enum STATE
    {
        Off,
        Left,
        Right,
    }
    public STATE state;
    public virtual void ChangeState(STATE newState) 
    {
        state = newState;
    }

    public virtual void LeftRoutine()
    {
        Debug.Log("LEFT");
    }

    public virtual void RightRoutine()
    {
        Debug.Log("RIGHT");
    }

    public void Update()
    {
        if (state == STATE.Left)
        {
            LeftRoutine();
        }
        if (state == STATE.Right)
        {
            RightRoutine();
        }
    }
}
