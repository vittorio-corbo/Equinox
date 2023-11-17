using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : Reportee
{
    // Start is called before the first frame update
    [SerializeField] private Vector3 openPos;
    private Vector3 closedPos;
    float speed = 0.5f;
    bool open = false;

    public override void Start()
    {
        closedPos = new Vector3(transform.position.x,
            transform.position.y,
            transform.position.z);
        base.Start();
    }

    void Update()
    {
        if (allFixed)
        {
            transform.position = Vector3.Lerp(transform.position, openPos, speed * Time.deltaTime);
        }
    }
}
