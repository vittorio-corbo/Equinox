using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Laser : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private PlayerGrapple playerGrapple;
    private GameObject playerCamera;

    public GameObject laserDot;
    public GameObject laserPlane;
    private Light laserDotLight;

    private Color transRed = new Color(1f, 0, 0, 0);
    private Color transBlue = new Color(0, 0, 1f, 0);
    private Color transWhite = new Color(1f, 1f, 1f, 0);
    private Color transBlack = new Color(0, 0, 0, 0);


    public float MAXDISTANCE;
    RaycastHit hit;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        playerGrapple = GetComponentInParent<PlayerGrapple>();
        playerCamera = playerGrapple.transform.GetChild(0).gameObject;
        lineRenderer.startWidth = .01f;
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

        laserRaycast(playerCameraOrigin, direction);

    }

    private void laserRaycast(Vector3 origin, Vector3 direction)
    {
        if (Physics.Raycast(origin, direction, out hit, MAXDISTANCE))
        {
            Vector3 referenceScaleVector = hit.point - origin;

            
            if (!hit.collider.CompareTag("CUBE") && hit.collider.GetComponent<GrappleHead>() == null)
            {//IGNORE SELF AND GRAPPLE HEAD

                laserPlane.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                laserPlane.transform.position = hit.point - referenceScaleVector.normalized * 0.6f;
                laserPlane.transform.localScale = referenceScaleVector.magnitude * (Vector3.one) / 10;

                laserDot.transform.position = hit.point - referenceScaleVector.normalized * 0.5f;
                laserDotLight.range = referenceScaleVector.magnitude / 26;


                if (hit.collider.CompareTag("Stopper"))
                {
                    //SET TO BLACK
                    lineRenderer.startColor = Color.black;
                    lineRenderer.endColor = transBlack;
                    laserDotLight.color = Color.black;
                }

                else if (hit.collider.CompareTag("Glass"))
                { //IT IS GLASS
                    lineRenderer.startColor = Color.white;
                    lineRenderer.endColor = transWhite;
                    laserDotLight.color = Color.white;
                }
                else if (hit.collider.CompareTag("Grabbable"))
                { // Grabbable Object
                    lineRenderer.startColor = Color.blue;
                    lineRenderer.endColor = transBlue;
                    laserDotLight.color = Color.blue;
                }
                else
                { //NO IMPORTANT COLLISIONS HAPPENING
                    lineRenderer.startColor = Color.red;
                    lineRenderer.endColor = transRed;
                    laserDotLight.color = Color.red;
                }
                laserDot.SetActive(true);
                laserPlane.SetActive(true);
            }
        }
        else
        {
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = transRed;
            laserDot.SetActive(false);
            laserPlane.SetActive(false);
        }
    }
}
