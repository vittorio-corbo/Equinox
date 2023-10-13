using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private Color color1;
    private Color color2;
    private LineRenderer lineRenderer;
    private PlayerGrapple playerGrapple;
    private GameObject playerCamera;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        playerGrapple = GetComponentInParent<PlayerGrapple>();
        playerCamera = playerGrapple.transform.GetChild(0).gameObject;
        lineRenderer.startWidth = .02f;
        lineRenderer.endWidth = 0f;
        lineRenderer.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 origin = transform.position;
        Vector3 playerCameraOrigin = playerCamera.transform.position;
        Vector3 direction = transform.forward;
        Vector3 endPoint = playerCameraOrigin + direction * 1.5f;
        RaycastHit hit;
        lineRenderer.SetPosition(0, origin);
        //used to be 100
        // if (Physics.Raycast(playerCameraOrigin, direction, out hit, playerGrapple.crc.MAXDISTANCE)) 
        // {

        // }
        lineRenderer.SetPosition(1, endPoint);


            /*//CUBE CROSSHAIR
            if (!hit.collider.CompareTag("CUBE") && hit.collider.GetComponent<GrappleHead>() == null)
            {//IGNORE SELF AND GRAPPLE HEAD
                //SET CUBE POSITION
                cube.transform.position = hit.point;

                //SET CUBE SCALE (based on distance)
                cube.transform.localScale = newVector.magnitude * (Vector3.one) / 16;


                if (hit.collider.CompareTag("Stopper"))
                {
                    //SET TO BLACK
                    cubeRenderer.material.SetColor("_Color", new Color(0f, 0f, 0f, cubeAlpha));
                    cubeRenderer.enabled = true;
                }

                else if (hit.collider.CompareTag("Glass"))
                { //IT IS GLASS
                    //CHANGE COLOR
                    cubeRenderer.material.SetColor("_Color", new Color(1f, 1f, 1f, cubeAlpha));
                    cubeRenderer.enabled = true;
                }
                else if (hit.collider.CompareTag("Grabbable"))
                { // Grabbable Object
                    // Blue because I'm bad at color theory. Sue me
                    // This is not legal advice. I am not a practicing lawyer in the state of Georgia.
                    cubeRenderer.material.color = Color.blue;
                    cubeRenderer.enabled = true;
                }
                else
                { //NO IMPORTANT COLLISIONS HAPPENING
                    //SET CUBE TO RED
                    cubeRenderer.material.SetColor("_Color", new Color(1f, 0f, 0f, cubeAlpha));
                    cubeRenderer.enabled = true;

                    //SET GOAL TO ORANGE


                }

                //Sets goal to teal when looking at it
                if (hit.collider.name == "Goal")
                {
                    cubeRenderer.enabled = false;
                    goalRenderer.material.SetColor("_Color", new Color(0f, 1f, 1f));
                }
                else
                {
                    goalRenderer.material.SetColor("_Color", new Color(1f, 0.318f, 0f));
                }
            }
        }*/
    }
}
