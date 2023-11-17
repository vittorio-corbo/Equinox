using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRopeNode : RopeNode
{
    public void StartRope()
    {
        last = FindObjectOfType<HeadRopeNode>();
        last.next = this;
    }
    private void OnCollisionExit(Collision collision)
    {
        //Overriding the other OnCollisionExit
    }

    public Vector3 WhereTo()
    {
        return last.transform.position;
    }
}
