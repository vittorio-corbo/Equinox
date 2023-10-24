using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using UnityEngine;
using static CrossHair;

public class Laser : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private PlayerGrapple playerGrapple;
    private GameObject playerCamera;

    public GameObject laserDot;
    private Light laserDotLight;

    public float MAXDISTANCE;
    RaycastHit hit;

    public delegate void SwitchCrossHair(CrossHairStates state);
    public static event SwitchCrossHair switchCrossHair;
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

        laserRaycast(playerCameraOrigin, direction);

    }

    private void laserRaycast(Vector3 origin, Vector3 direction)
    {
        if (Physics.Raycast(origin, direction, out hit, MAXDISTANCE))
        {
            Vector3 referenceScaleVector = hit.point - transform.position;
            if (!hit.collider.CompareTag("CUBE") && hit.collider.GetComponent<GrappleHead>() == null)
            {//IGNORE SELF AND GRAPPLE HEAD
                laserDot.transform.position = hit.point;
                laserDotLight.range = referenceScaleVector.magnitude / 32;


                if (hit.collider.CompareTag("Stopper"))
                {
                    //SET TO BLACK
                    laserDotLight.color = new Color(0, 0, 0, 0.5f);
                    switchCrossHair?.Invoke(CrossHairStates.Stopper);
                }

                else if (hit.collider.CompareTag("Glass"))
                { //IT IS GLASS
                    laserDotLight.color = Color.white;
                    switchCrossHair?.Invoke(CrossHairStates.Glass);
                }
                else if (hit.collider.CompareTag("Grabbable"))
                { // Grabbable Object
                    laserDotLight.color = Color.blue;
                    switchCrossHair?.Invoke(CrossHairStates.Grabbable);
                }
                else
                { //NO IMPORTANT COLLISIONS HAPPENING
                    laserDotLight.color = Color.red;
                    switchCrossHair?.Invoke(CrossHairStates.DefaultObject);
                }
                laserDot.SetActive(true);
            }
        }
        else
        {
            laserDot.SetActive(false);
            switchCrossHair?.Invoke(CrossHairStates.NoObject);
        }
    }
}
