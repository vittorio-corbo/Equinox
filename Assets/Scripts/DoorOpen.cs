using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : Reportee
{
    // Start is called before the first frame update
    [SerializeField] private Vector3 openPos;
    float speed = 0.5f;
    
    void Update()
    {
        if (allFixed)
        {
            transform.position = Vector3.Lerp(transform.position, openPos, speed * Time.deltaTime);
        }
    }
}
