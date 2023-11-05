using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairCubeRayCast : MonoBehaviour
{

    public float MAXDISTANCE = 70;
    public float MAXCUBEDIST = 5000;
    [SerializeField] private GameObject cube;
    private Renderer cubeRenderer;
    private Material defaultCubeMat;
    private GameObject player;
    public static float cubeAlpha = 0.5f;
    public Material alphaMat;

    //outOfRange: You cannot grapple to this
    public bool outOfRange = false;
    //shooting: The grapple is already shooting
    public bool shooting = false;

    public RaycastHit hit;
    public bool hitSomething;
    public Vector3 hitPoint;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent.gameObject;
        cube = Instantiate(cube, new Vector3(0,0,0), Quaternion.identity);
        cubeRenderer = cube.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (outOfRange || shooting)
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
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            hitSomething = true;
            Vector3 newVector = hit.point - transform.position;
            if (!hit.collider.CompareTag("CUBE") && hit.collider.GetComponent<GrappleHead>() == null)
            {
                cube.transform.position = hit.point;
                cube.transform.localScale = newVector.magnitude * (Vector3.one) / 16;
                if (hit.distance > MAXDISTANCE)
                {
                    outOfRange = true;
                }
                else
                {
                    outOfRange = false;
                }
                setColor();

                cubeRenderer.enabled = true;
            }
        }
        else
        {
            hitSomething = false;
            cubeRenderer.enabled = false;
        }
        /*
        //used to be 100
        if (Physics.Raycast(transform.position, transform.forward, out hit, MAXDISTANCE)) //not check for layer anymore
        {
            hitSomething = true;
            //HOLDS THE DISTANCE
            Vector3 newVector = hit.point - transform.position;


            //CUBE CROSSHAIR
            if (!hit.collider.CompareTag("CUBE") && hit.collider.GetComponent<GrappleHead>() == null)
            {//IGNORE SELF AND GRAPPLE HEAD
                //SET CUBE POSITION
                cube.transform.position = hit.point;

                //SET CUBE SCALE (based on distance)
                cube.transform.localScale = newVector.magnitude * (Vector3.one) / 32;


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
            }
        }
        //If the object is too far to grapple, turn the crosshair cube gray but keep tracking it
        else if (Physics.Raycast(transform.position, transform.forward, out hit))
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
        */
        return hitSomething;
    }

    //Sets the color of the crosshair cube based on the object tag and mass passed in
    //returns nothing
    private void setColor()
    {
        if (hit.collider.CompareTag("Stopper"))
        {
            cubeRenderer.material.SetColor("_Color", colors["RED"]);
        }
        else if (hit.collider.CompareTag("MoveableObject"))
        {
            if (hit.rigidbody.mass >= player.GetComponent<Rigidbody>().mass * 1.5)
            {
                cubeRenderer.material.SetColor("_Color", colors["ORANGE"]);
            }
            else if (hit.rigidbody.mass <= player.GetComponent<Rigidbody>().mass / 1.5)
            {
                cubeRenderer.material.SetColor("_Color", colors["GREEN"]);
            }
            else
            {
                cubeRenderer.material.SetColor("_Color", colors["YELLOW"]);
            }
        }
        else if (hit.collider.CompareTag("Grabbable"))
        {
            cubeRenderer.material.SetColor("_Color", colors["BLUE"]);
        }
        else
        {
            cubeRenderer.material.SetColor("_Color", colors["CYAN"]);
        }

    }

    Dictionary<string, Color> colors = new Dictionary<string, Color>()
    {
        {"RED", new Color(1.0f, 0.0f, 0.0f, cubeAlpha) },
        {"BLUE", new Color(0.0f, 0.0f, 1.0f, cubeAlpha) },
        {"GREEN", new Color(0.0f, 1.0f, 0.0f, cubeAlpha) },
        {"ORANGE", new Color(1.0f, .5f, 0.0f, cubeAlpha) },
        {"YELLOW", new Color(1.0f, 1.0f, 0.0f, cubeAlpha) },
        {"CYAN", new Color(0.0f, 1.0f, 1.0f, cubeAlpha) }
    };
}
