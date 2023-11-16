using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private Vector3 EndPosition;
    private bool moving;
    [SerializeField] private Vector3 StartPosition;

    [SerializeField] private float timer;
    public void StartMovement()
    {
        startTime = Time.time;
        moving = true;
        StartCoroutine(ExplodeEnum());
    }

    // Movement speed in units per second.
    public float speed = 1.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    void Awake()
    {
        StartPosition = transform.position;

        moving = false;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(StartPosition, EndPosition);
    }

    // Move to the target end position.
    void Update()
    {
        if (moving)
        {
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(StartPosition, EndPosition, fractionOfJourney);
        }
    }

    private void Explode()
    {
        StopCoroutine(ExplodeEnum());
        if (FindObjectOfType<PlayerGrapple>().gameObject.GetComponent<ConfigurableJoint>() != null)
        {
            if (FindObjectOfType<PlayerGrapple>().gameObject.GetComponent<ConfigurableJoint>().connectedBody.Equals(gameObject.GetComponent<Rigidbody>())) {
                FindObjectOfType<PlayerGrapple>().StopHolding();
            }
        }
        transform.position = StartPosition;
        moving = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (moving && collision.gameObject.GetComponent<PlayerGrapple>() == null)
        {
            Explode();
        }
    }

    private IEnumerator ExplodeEnum()
    {
        yield return new WaitForSeconds(timer);
        Explode();
    }
}
