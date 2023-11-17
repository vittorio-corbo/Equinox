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

    void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (allFixed && !open && react)
        {
            transform.position = Vector3.Lerp(transform.position, openPos, speed * Time.deltaTime);
            if (transform.position == openPos)
            {
                open = !open;
                react = false;
            }
        }
        else if (allFixed && open && react)
        {
            transform.position = Vector3.Lerp(transform.position, closedPos, speed * Time.deltaTime);
            if (transform.position == closedPos)
            {
                open = !open;
                react = false;
            }
        }
    }
}
