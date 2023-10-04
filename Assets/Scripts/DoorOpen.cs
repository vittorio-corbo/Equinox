using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : Reportee
{
    // Start is called before the first frame update
    private Vector3 openPos;
    private Vector3 closedPos;
    private Rigidbody rigidBody;
    private bool closed = true;
    float speed = 0.5f;
    void Start()
    {
        base.Start();
        rigidBody = GetComponent<Rigidbody>();
        openPos = transform.position + transform.right * GetComponent<MeshRenderer>().bounds.size.x;
        
    }
    
    void Update()
    {
        if (allFixed && closed)
        {
            transform.position = Vector3.Lerp(transform.position, openPos, speed * Time.deltaTime);
        }
    }

    public void GatherReport()
    {
        base.GatherReport();
    }

    private void SetDoorState()
    {
        if (closed)
        {
            closed = false;
        }
        else
        {
            closed = true;
        }
    }

}
