using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeNode : MonoBehaviour
{
    public RopeNode next;
    public RopeNode last;
    private LineRenderer lr;
    public Material materialRef;
    public void UpdateNode()
    {
        /*
        if (next == null)
        {
            return;
        }
        RaycastHit hit;
        if (Physics.Raycast(transform.position, next.transform.position - transform.position, out hit, (next.transform.position - transform.position).magnitude, (~LayerMask.GetMask("Pizza"))))
        {
            GameObject node = Instantiate(Resources.Load("Node")) as GameObject;
            node.transform.position = hit.point;
            node.GetComponent<FixedJoint>().connectedBody = hit.collider.gameObject.GetComponent<Rigidbody>();
            RopeNode ropeNode = node.GetComponent<RopeNode>();
            ropeNode.next = next;
            ropeNode.next.last = ropeNode;
            ropeNode.last = this;
            next = ropeNode;
            next.next.UpdateNode();
            return;
        }
        next.UpdateNode();

        UpdateForce();
        */
    }

    public void UpdateLine()
    {        
        if (next == null)
        {
            return;
        }
        if (lr == null)
        {
            lr = gameObject.AddComponent<LineRenderer>();
        }
        lr.startWidth = 0.01f;
        lr.endWidth = 0.01f;
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, next.transform.position);
        lr.material = materialRef;
        next.UpdateLine();
    }

    public void UpdateForce()
    {
        if (next == null || last == null || next.next == null)
        {
            return;
        }
    }

    public void DestroyNode()
    {
        last.next = next;
        next.last = last;
        Destroy(this.gameObject);
    }

    private void OnJointBreak(float breakForce)
    {
        DestroyNode();
    }
}
