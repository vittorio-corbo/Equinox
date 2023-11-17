using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorStates : MonoBehaviour
{
    public DoorState currentState = DoorState.CLOSED;
    public Vector3 openPos;
    public Vector3 closedPos;
    private float speed = 0.5f;
    public enum DoorState
    {
        OPEN,
        CLOSED
    }

    public void ChangeState(DoorState state)
    {
        currentState = state;
    }

    public void OpenRoutine()
    {
        if (transform.position != openPos)
            transform.position = Vector3.Lerp(transform.position, openPos, speed * Time.deltaTime);
    }

    public void ClosedRoutine()
    {
        if (transform.position != closedPos)
            transform.position = Vector3.Lerp(transform.position, closedPos, speed * Time.deltaTime);
    }
    // Update is called once per frame
    void Update()
    {
        if (currentState == DoorState.OPEN)
        {
            OpenRoutine();
        }
        if (currentState == DoorState.CLOSED)
        {
            ClosedRoutine();
        }
    }

    void Start()
    {
        closedPos = transform.position;
    }
}
