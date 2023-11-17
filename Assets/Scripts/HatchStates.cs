using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchStates : MonoBehaviour
{
    public enum HatchState
    {
        OPEN,
        CLOSED
    }
    public HatchState currentState = HatchState.CLOSED;
    HingeJoint hj;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hj = GetComponent<HingeJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == HatchState.OPEN)
        {
            OpenRoutine();
        }
        if (currentState == HatchState.CLOSED)
        {
            ClosedRoutine();
        }
    }

    public void ChangeState(HatchState state)
    {
        currentState = state;
    }

    void ClosedRoutine()
    {
        var m = hj.motor;
        m.force = 10;
        m.targetVelocity = 10;
        hj.motor = m;
        rb.isKinematic = false;
        hj.useMotor = true;
        if (hj.angle == hj.limits.max)
        {
            hj.useMotor = false;
            rb.isKinematic = true;
        }
    }

    void OpenRoutine()
    {
        var m = hj.motor;
        m.force = 10;
        m.targetVelocity = -10;
        hj.motor = m;
        rb.isKinematic = false;
        hj.useMotor = true;
        if (hj.angle == hj.limits.min)
        {
            hj.useMotor = false;
            rb.isKinematic = true;
        }
    }
}
