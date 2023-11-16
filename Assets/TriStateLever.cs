using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriStateLever : MonoBehaviour
{
    public enum STATE
    {
        Off,
        Left,
        Right,
    }
    public STATE state;
    public void ChangeState(STATE newState) 
    {
        Debug.Log(newState);
        state = newState;
    }

    public void LeftRoutine()
    {
        Debug.Log("LEFT");
    }

    public void RightRoutine()
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
