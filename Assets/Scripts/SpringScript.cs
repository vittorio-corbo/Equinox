using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringScript : MonoBehaviour
{
    SpringJoint spring;
    [SerializeField] GameObject springBase;

    float currentMin;
    public bool winching;

    float dampingForce;
    float minDistance;
    float maxDistance;
    float force;
    float tolerance;

    private void Start()
    {
        spring = springBase.GetComponent<SpringJoint>();
        if (spring == null)
        {
            Debug.LogWarning("SPRING JOINT ON SPRING " + transform.parent.parent.gameObject.name + " DOES NOT EXIST");
        }
        else
        {
            dampingForce = spring.damper;
            minDistance = spring.minDistance;
            maxDistance = spring.maxDistance;
            currentMin = maxDistance;
            tolerance = spring.tolerance;
            force = spring.spring;
            Destroy(spring);
            spring = null;
        }
    }
    void Update()
    {
        if (winching)
        {
            Destroy(spring);
            spring = null;
            
            if (transform.localPosition.z - springBase.transform.localScale.z / 2 - transform.localScale.z / 2 < 0)
            {
                currentMin = 0;
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, springBase.transform.localScale.z / 2 + transform.localScale.z / 2);
            }
            else if (transform.localPosition.z - springBase.transform.localScale.z / 2 - transform.localScale.z / 2 < currentMin)
            {
                currentMin = transform.localPosition.z - springBase.transform.localScale.z / 2 - transform.localScale.z / 2;
            }
            else
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, currentMin + springBase.transform.localScale.z / 2 + transform.localScale.z / 2);
            }
            if (FindObjectOfType<GrappleHead>().GetComponent<FixedJoint>() == null || !FindObjectOfType<GrappleHead>().GetComponent<FixedJoint>().connectedBody.Equals(gameObject))
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
        else
        {
            if (spring == null)
            {
                spring = springBase.AddComponent<SpringJoint>();
                spring.damper = dampingForce;
                spring.spring = force;
                spring.minDistance = minDistance;
                spring.maxDistance = maxDistance;
                spring.tolerance = tolerance;
                spring.connectedBody = GetComponent<Rigidbody>();
                spring.autoConfigureConnectedAnchor = false;
                spring.connectedAnchor = new Vector3(0, 0, -0.49f);
                spring.enableCollision = true;
                spring.anchor = new Vector3(0, 0, 0.5f);
            }
            currentMin = maxDistance;
        }
    }
}
