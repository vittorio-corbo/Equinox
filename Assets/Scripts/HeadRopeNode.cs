using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRopeNode : RopeNode
{
    private void OnCollisionExit(Collision collision)
    {
        //Overriding RopeNode OnCollisionExit
    }

    public Vector3 WhereTo()
    {
        return next.transform.position;
    }
}
