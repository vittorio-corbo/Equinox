using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairCubeRayCast : MonoBehaviour
{

    public float MAXDISTANCE;
    public float MAXCUBEDIST;
    [SerializeField] private GameObject cube;
    private Renderer cubeRenderer;
    private Material defaultCubeMat;
    public float cubeAlpha;
    public Material alphaMat;

    //outOfRange actually means that you are currently grappled and can't shoot right now
    public bool outOfRange = false;

    public RaycastHit hit;
    public bool hitSomething;
    public Vector3 hitPoint;

    //GOAL REFERENCE
    public GameObject goal;
    private Renderer goalRenderer;

    // Start is called before the first frame update
    void Start()
    {
        cubeRenderer = cube.GetComponent<Renderer>();
        goalRenderer = goal.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (outOfRange)
        {
            CrosshairAlpha(0.5f);
        }
        else
        {
            CrosshairAlpha(1.0f);
        }

        hitSomething = ChangeCube();
        //cubeRenderer.enabled = false;

    }

    //Sets the transparency of the crosshair to the desired value. 
    //Changes material of crosshair to match what is needed
    public void CrosshairAlpha(float alpha)
    {


        if (alpha < 1)
        {
            Color c = cubeRenderer.material.color;
            cubeRenderer.material = alphaMat;
            cubeRenderer.material.SetColor("_Color", c);
        }
        else
        {
            Color c = cubeRenderer.material.color;
            cubeRenderer.material = defaultCubeMat;
            cubeRenderer.material.SetColor("_Color", c);
        }
        cubeAlpha = alpha;
    }

    public bool ChangeCube()
    {
        bool hitSomething = false;
        //used to be 100
        if (Physics.Raycast(transform.position, transform.forward, out hit, MAXDISTANCE)) //not check for layer anymore
        {
            hitSomething = true;
            Debug.Log(hit);
            //HOLDS THE DISTANCE
            Vector3 newVector = hit.point - transform.position;


            //CUBE CROSSHAIR
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
        }
        //If the object is too far to grapple, turn the crosshair cube gray but keep tracking it
        else if (Physics.Raycast(transform.position, transform.forward, out hit, MAXCUBEDIST))
        {
            hitSomething = true;
            //HOLDS THE DISTANCE
            Vector3 newVector = hit.point - transform.position;
            cubeRenderer.enabled = true;
            cubeRenderer.material.SetColor("_Color", new Color(0.5f, 0.5f, 0.5f, cubeAlpha));

            //CUBE CROSSHAIR
            if (!hit.collider.CompareTag("CUBE") && hit.collider.GetComponent<GrappleHead>() == null)
            {//IGNORE SELF AND GRAPPLE HEAD
                //SET CUBE POSITION
                cube.transform.position = hit.point;

                //SET CUBE SCALE (based on distance)
                cube.transform.localScale = newVector.magnitude * (Vector3.one) / 16;

                cube.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal) * transform.rotation;
                //cube.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
                //Debug.Log(hit.normal);
                //cube.transform.rotation = Quaternion.FromToRotation(Vector3.left, hit.normal);
                //myTargetReticle.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                //transform.rotation = Quaternion.FromToRotation(Vector3.forward, normal);
            }

        }
        //If the raycast doesn't work (Returns false, meaning no hit), then stop showing the cube yo
        //A software tester walks into a bar, runs into a bar, crawls into a bar, attempts to noclip through a bar, and teleports to a bar
        //He then orders one beer, two beers, 0 beers, -5 beers, 999999999 beers, and a water.
        //A real customer enters the bar and asks where the bathroom is.
        //The bar catches fire.
        else
        {
            hitSomething = false;
            cubeRenderer.enabled = false;
        }
        return hitSomething;
    }
}
