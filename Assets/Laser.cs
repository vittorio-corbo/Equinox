using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private PlayerGrapple playerGrapple;
    private GameObject playerCamera;

    public GameObject laserDot;
    private Light laserDotLight;

    public float MAXDISTANCE;
    RaycastHit hit;
     
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        playerGrapple = GetComponentInParent<PlayerGrapple>();
        playerCamera = playerGrapple.transform.GetChild(0).gameObject;
        lineRenderer.startWidth = .02f;
        lineRenderer.endWidth = 0f;
        lineRenderer.positionCount = 2;
        laserDotLight = laserDot.GetComponent<Light>();
    }

    void Update()
    {
        Vector3 origin = transform.position;
        Vector3 playerCameraOrigin = playerCamera.transform.position;
        Vector3 direction = transform.forward;
        Vector3 endPoint = origin + direction * 1.5f;
        
        lineRenderer.SetPosition(0, origin);
        lineRenderer.SetPosition(1, endPoint);

        if (Physics.Raycast(playerCameraOrigin, direction, out hit, MAXDISTANCE)) 
        {
            Vector3 referenceScaleVector = hit.point - transform.position;
            if (!hit.collider.CompareTag("CUBE") && hit.collider.GetComponent<GrappleHead>() == null)
            {//IGNORE SELF AND GRAPPLE HEAD
                laserDot.transform.position = hit.point;
                laserDotLight.range = referenceScaleVector.magnitude / 32;


                if (hit.collider.CompareTag("Stopper"))
                {
                    //SET TO BLACK
                    laserDotLight.color = Color.black;
                }

                else if (hit.collider.CompareTag("Glass"))
                { //IT IS GLASS
                    //CHANGE COLOR
                    laserDotLight.color = Color.white;
                }
                else if (hit.collider.CompareTag("Grabbable"))
                { // Grabbable Object
                    // Blue because I'm bad at color theory. Sue me
                    // This is not legal advice. I am not a practicing lawyer in the state of Georgia.
                    laserDotLight.color = Color.blue;
                }
                else
                { //NO IMPORTANT COLLISIONS HAPPENING
                    //SET CUBE TO RED
                    laserDotLight.color = Color.red;
                }
                laserDot.SetActive(true);
            }
        } else
        {
            laserDot.SetActive(false);
        }
        

    }
}
