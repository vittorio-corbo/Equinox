using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrappleHead : MonoBehaviour
{
    public bool insideSomething;
    public float SPEED;
    private PlayerGrapple player;
    private Rigidbody rigidBody;

    void Awake()
    {
        player = FindObjectOfType<PlayerGrapple>();
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (player.MAXDISTANCE < (player.transform.position - transform.position).magnitude)
        {
            StopGrappling();
        }
    }
    public void StartMovement(Vector3 startPosition, Vector3 direction)
    {
        StopGrappling();
        gameObject.SetActive(true);
        transform.position = startPosition + direction.normalized * 1f;
        rigidBody.AddForce(direction * SPEED);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerGrapple>() == null)
        {
            if (!insideSomething)
            {
                rigidBody.isKinematic = true; //Disables Physics on this object
                GetComponent<Collider>().enabled = false;
                transform.parent = collision.transform;
                insideSomething = true;
                player.StartGrappling(collision.collider);
            }
        }
    }

    public void StopGrappling()
    {
        insideSomething = false;
        rigidBody.isKinematic = false; //enables physics
        transform.parent = null;
        GetComponent<Collider>().enabled = true;
        rigidBody.velocity = Vector3.zero;
        player.StopGrappling();
        gameObject.SetActive(false);
    }
}
