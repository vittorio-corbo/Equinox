using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchController : Reportee
{
    Rigidbody rb;
    bool open = false;
    HingeJoint hj;
    float targetAngle;
    private void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        hj = GetComponent<HingeJoint>();
    }

    private void Update()
    {
        if (!open && allFixed && react)
        {
            var m = hj.motor;
            m.force = 10;
            m.targetVelocity = -10;
            hj.motor = m;
            rb.isKinematic = false;
            hj.useMotor = true;
            if (hj.angle == hj.limits.min)
            {
                open = !open;
                react = false;
                hj.useMotor = false;
                rb.isKinematic = true;
            }
        }
        else if (open && allFixed && react)
        {
            var m = hj.motor;
            m.force = 10;
            m.targetVelocity = 10;
            hj.motor = m;
            rb.isKinematic = false;
            hj.useMotor = true;
            if (hj.angle == hj.limits.max)
            {
                open = !open;
                react = false;
                hj.useMotor = false;
                rb.isKinematic = true;
            }
        }
    }
}
