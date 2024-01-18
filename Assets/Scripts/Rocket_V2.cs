using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket_V2 : MonoBehaviour
{
    //BUG: THE ROCKET WILL RESET POSITION WITH NO IDENTIFIABLE REASON

    [SerializeField] private Vector3 EndPosition;
    private bool moving;
    [SerializeField] private Vector3 StartPosition;

    [SerializeField] private float timer;

    [SerializeField] private float force;
    Rigidbody m_Rigidbody;
    public void StartMovement()
    {
        //print("i started");
        startTime = Time.time;
        moving = true;
        StartCoroutine(ExplodeEnum());
        //m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.isKinematic = true;
        m_Rigidbody.isKinematic = false;
        m_Rigidbody.velocity = Vector3.zero;
    }

    // Movement speed in units per second.
    public float speed = 1.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    void Start(){

        m_Rigidbody = GetComponent<Rigidbody>();
    }
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
        //m_Rigidbody.AddForce(this.transform.forward * 20f);
        

        if (moving)
        {
            //Vector3 _directionF = m_Rigidbody.rotation * new Vector3(-1, 0, -1);
            //m_Rigidbody.MovePosition(m_Rigidbody.position + (_directionF * speed * Time.fixedDeltaTime));

            //m_Rigidbody.AddForce(this.transform.forward * 20f);
            m_Rigidbody.AddForce(this.transform.forward * force);
            //m_Rigidbody.AddForce(transform.up * 20f);

            // // Distance moved equals elapsed time times speed..
            // float distCovered = (Time.time - startTime) * speed;

            // // Fraction of journey completed equals current distance divided by total distance.
            // float fractionOfJourney = distCovered / journeyLength;

            // // Set our position as a fraction of the distance between the markers.
            // transform.position = Vector3.Lerp(StartPosition, EndPosition, fractionOfJourney);
        }
    }

    private void Explode()
    {
        print("i explode");
        StopCoroutine(ExplodeEnum());
        if (FindObjectOfType<PlayerGrapple>().gameObject.GetComponent<ConfigurableJoint>() != null)
        {
            if (FindObjectOfType<PlayerGrapple>().gameObject.GetComponent<ConfigurableJoint>().connectedBody.Equals(gameObject.GetComponent<Rigidbody>())) {
                FindObjectOfType<PlayerGrapple>().StopHolding();
            }
        }
        transform.position = StartPosition;
        m_Rigidbody.velocity = Vector3.zero;
        m_Rigidbody.isKinematic = true;
        m_Rigidbody.isKinematic = false;
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
