using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Laser : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private GameObject player;
    private GameObject playerCamera;

    public GameObject laserDot;
    public GameObject laserPlane;
    private Light laserDotLight;
    private Renderer planeRenderer;

    public bool outOfRange = false;
    public bool shooting = false;
    public bool hitSomething = false;
    public float MAXDISTANCE;
    public RaycastHit hit;


    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        planeRenderer = laserPlane.GetComponent<Renderer>();
        player = GetComponentInParent<PlayerGrapple>().gameObject;
        playerCamera = player.transform.GetChild(0).gameObject;
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
        //Debug.Log(hitSomething);
    }

    private void laserRaycast(Vector3 origin, Vector3 direction)
    {
        if (Physics.Raycast(origin, direction, out hit))
        //if (Physics.Raycast(origin, direction, out hit,~LayerMask.GetMask("NotHoldable")))
        {
            hitSomething = true;
            Vector3 referenceScaleVector = hit.point - origin;
            
            if (!hit.collider.CompareTag("CUBE") && hit.collider.GetComponent<GrappleHead>() == null)
            {//IGNORE SELF AND GRAPPLE HEAD
                laserPlane.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                laserPlane.transform.position = hit.point - referenceScaleVector.normalized * 0.6f;
                laserPlane.transform.localScale = referenceScaleVector.magnitude * (Vector3.one) / 10;

                laserDot.transform.position = hit.point - referenceScaleVector.normalized * 0.5f;
                laserDotLight.range = referenceScaleVector.magnitude / 26;

                if (hit.distance > MAXDISTANCE)
                {
                    hitSomething = false;
                    outOfRange = true;
                }
                else
                {
                    hitSomething = true;
                    outOfRange = false;
                }

                setColor(hit);
                
                laserDot.SetActive(true);
                laserPlane.SetActive(true);
            }
        }
        else
        {
            hitSomething = false;
            lineRenderer.startColor = colors["RED"];
            lineRenderer.endColor = transparentColors["RED"];
            laserDot.SetActive(false);
            laserPlane.SetActive(false);
        }
    }


    private void setColor(RaycastHit hit)
    {
        if (hit.collider.CompareTag("Stopper"))
        {
            planeRenderer.material.color = getSaturationColor(colors["RED"]);
            laserDotLight.color = getSaturationColor(colors["RED"]);
            lineRenderer.startColor = colors["RED"];
            lineRenderer.endColor = transparentColors["RED"];
        }
        else if (hit.collider.CompareTag("MoveableObject"))
        {
            if (hit.rigidbody.mass >= player.GetComponent<Rigidbody>().mass * 1.5)
            {
                planeRenderer.material.color = getSaturationColor(colors["ORANGE"]);
                laserDotLight.color = getSaturationColor(colors["ORANGE"]);
                lineRenderer.startColor = colors["ORANGE"];
                lineRenderer.endColor = transparentColors["ORANGE"];
            }
            else if (hit.rigidbody.mass <= player.GetComponent<Rigidbody>().mass / 1.5)
            {
                planeRenderer.material.color = getSaturationColor(colors["GREEN"]);
                laserDotLight.color = getSaturationColor(colors["GREEN"]);
                lineRenderer.startColor = colors["GREEN"];
                lineRenderer.endColor = transparentColors["GREEN"];
            }
            else
            {
                planeRenderer.material.color = getSaturationColor(colors["YELLOW"]);
                laserDotLight.color = getSaturationColor(colors["YELLOW"]);
                lineRenderer.startColor = colors["YELLOW"];
                lineRenderer.endColor = transparentColors["YELLOW"];
            }
        }
        else if (hit.collider.CompareTag("Grabbable"))
        {
            planeRenderer.material.color = getSaturationColor(colors["BLUE"]);
            laserDotLight.color = getSaturationColor(colors["BLUE"]);
            lineRenderer.startColor = colors["BLUE"];
            lineRenderer.endColor = transparentColors["BLUE"];
        }
        else
        {
            planeRenderer.material.color = getSaturationColor(colors["CYAN"]);
            laserDotLight.color = getSaturationColor(colors["CYAN"]);
            lineRenderer.startColor = colors["CYAN"];
            lineRenderer.endColor = transparentColors["CYAN"];
        }

    }
    Dictionary<string, Color> colors = new Dictionary<string, Color>()
    {
        {"RED", new Color(1.0f, 0.0f, 0.0f) },
        {"BLUE", new Color(0.0f, 0.0f, 1.0f) },
        {"GREEN", new Color(0.0f, 1.0f, 0.0f) },
        {"ORANGE", new Color(1.0f, .5f, 0.0f) },
        {"YELLOW", new Color(1.0f, 1.0f, 0.0f) },
        {"CYAN", new Color(0.0f, 1.0f, 1.0f) }
    };

    Dictionary<string, Color> transparentColors = new Dictionary<string, Color>()
    {
        {"RED", new Color(1.0f, 0.0f, 0.0f, 0f) },
        {"BLUE", new Color(0.0f, 0.0f, 1.0f, 0f) },
        {"GREEN", new Color(0.0f, 1.0f, 0.0f, 0f) },
        {"ORANGE", new Color(1.0f, .5f, 0.0f, 0f) },
        {"YELLOW", new Color(1.0f, 1.0f, 0.0f, 0f) },
        {"CYAN", new Color(0.0f, 1.0f, 1.0f, 0f) }
    };
    private Color getSaturationColor(Color color)
    {
        float H, S, V;
        Color.RGBToHSV(color, out H, out S, out V);
        if (!outOfRange && !shooting)
        {
            return Color.HSVToRGB(H, S / 1, V); //play with this
        } else
        {
            return Color.HSVToRGB(H, S, V / 2f); //play with this
        }
    }
}
