using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using static UnityEngine.GraphicsBuffer;

public class PlayerGrapple : MonoBehaviour
{
    public LayerMask Pizza;

    private Coroutine grappleCoroutine;

    //FOR ANIMATION (BLINKING)
    [SerializeField] private GameObject cinematicBarsContainerGO;
    private GameObject hand;
    float timer = -1.0f;

    //Hiding the camera's rigidbody with the one we want
    [SerializeField] private new Rigidbody rigidbody;

    public float MAXDISTANCE;
    public float MAXCUBEDIST;

    //CROSSHAIR
    public GameObject cube;
    private Renderer cubeRenderer;
    private Material defaultCubeMat;
    public float cubeAlpha;
    public Material alphaMat;
    //Note: This is used both when the point you are looking at is out of range and when you are already grappling
    public bool outOfRange = false;

    //GOAL REFERENCE
    public GameObject goal;
    private Renderer goalRenderer;

    public float grappleForce;

    //AUDIO
    private AudioSource source;

    private GrappleHead grappleHead;

    private Vector3 momentum;

    private Rigidbody holdRigid;
    private bool holding;
    private bool prevHolding;

    [SerializeField] float dampingAngle;
    [SerializeField] float dampingSpeed;

    //GLASS MATERIAL
    //public Material glass;

    //GRAVITY/CAMERA EXPERIMENT

    //Experimenting with cube rendering


    public void ShowBars() {
        cinematicBarsContainerGO.SetActive(true);
        Debug.Log("asdf");
    }

    // Start is called before the first frame update
    void Start()
    {
        //KILL BLINK IF IT PLAYS AT THE START
        hand = GameObject.Find("CinematicBlackBarsContainer");
        //print(hand);
        if (hand != null){
            hand.SetActive(false);
        }

        //GET SPHERE RENDERER (cube crosshair)
        cubeRenderer = cube.GetComponent<Renderer>();
        

        //GET GOAL Renderer
        goalRenderer = goal.GetComponent<Renderer>();

        //GET AUDIO SOURCE
        source = GetComponent<AudioSource>();

        //Use FindObjectsOfTypeAll so it finds inactive scripts too
        grappleHead = Resources.FindObjectsOfTypeAll(typeof(GrappleHead))[0] as GrappleHead;
        grappleHead.gameObject.SetActive(false);
        holding = Input.GetKeyDown(KeyCode.F);
        prevHolding = holding;
    }

    // Update is called once per frame
    //MOVE DA CAMERA
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
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        if (Input.GetKeyDown(KeyCode.F)) {
            holding = true;
        }
        if (Input.GetKeyUp(KeyCode.F)) {
            holding = false;
        }
        Debug.Log(holding);

        if (holding) {
            if (holding != prevHolding) {
                HoldSurface();
            }
            if (holdRigid != null) {
                rigidbody.velocity = holdRigid.velocity;
            }
           
        } else {
            holdRigid = null;
        }

        if (Input.GetMouseButtonDown(0) && !holding)
        {
            //PLAY SOUND
            //source.Play();
            outOfRange = true;
            grappleHead.StartMovement(transform.position, transform.forward);
        }

        ChangeCube();        

        if (timer != -1.0f) { 
            timer += Time.deltaTime;
            // int seconds = timer % 60;
            //print(timer);
            if (timer >= 0.20f)
            {
                hand = GameObject.Find("CinematicBlackBarsContainer");
                //print(hand);
                if (hand != null) { 
                hand.SetActive(false);
                }
            } 
        }

        prevHolding = holding;
    }

    private void HoldSurface() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 5f)) //not check for layer anymore
        {
            holdRigid = hit.transform.GetComponent<Rigidbody>();
        }
    }

    private void ChangeCube() {
        RaycastHit hit;
        //used to be 100
        if (Physics.Raycast(transform.position, transform.forward, out hit, MAXDISTANCE)) //not check for layer anymore
        {
            //HOLDS THE DISTANCE
            Vector3 newVector = hit.point - transform.position;


            //CUBE CROSSHAIR
            if (!hit.collider.CompareTag("CUBE") && hit.collider.GetComponent<GrappleHead>() == null)
            {//IGNORE SELF AND GRAPPLE HEAD
                //SET CUBE POSITION
                cube.transform.position = hit.point;
                
                //SET CUBE SCALE (based on distance)
                cube.transform.localScale = newVector.magnitude * (Vector3.one)/16;


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
                else if (hit.collider.CompareTag("Grabbable")) { // Grabbable Object
                    // Blue because I'm bad at color theory. Sue me
                    // This is not legal advice. I am not a practicing lawyer in the state of Georgia.
                    cubeRenderer.material.color = Color.blue;
                    cubeRenderer.enabled = true;
                }
                else
                { //NO IMPORTANT COLLISIONS HAPPENING
                    //SET CUBE TO RED
                    cubeRenderer.material.SetColor("_Color", new Color(1f, 0f, 0f,cubeAlpha));
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
            }

        }
        //If the raycast doesn't work (Returns false, meaning no hit), then stop showing the cube yo
        //A software tester walks into a bar, runs into a bar, crawls into a bar, attempts to noclip through a bar, and teleports to a bar
        //He then orders one beer, two beers, 0 beers, -5 beers, 999999999 beers, and a water.
        //A real customer enters the bar and asks where the bathroom is.
        //The bar catches fire.
        else
        {
            cubeRenderer.enabled = false;
            goalRenderer.material.SetColor("_Color", new Color(1f, 0.318f, 0f));
        }
    }

    public void StartGrappling(Collider collider)
    {
        grappleCoroutine = StartCoroutine(Grappling(collider));
    }

    private IEnumerator Grappling(Collider collider)
    {
        while (true) {
            Vector3 moveVector = grappleHead.transform.position - transform.position;
            if (collider.CompareTag("MoveableObject"))
            {
                if(Vector3.Angle(rigidbody.velocity.normalized, moveVector.normalized) > dampingAngle)
                {
                    Debug.Log(rigidbody.velocity);
                    rigidbody.velocity = rigidbody.velocity * (1-dampingSpeed);
                    Debug.Log(rigidbody.velocity);
                }
                collider.GetComponent<Rigidbody>().AddForceAtPosition(moveVector.normalized * -grappleForce / 2, grappleHead.transform.position);
                rigidbody.AddForce(moveVector.normalized * (grappleForce / 2));
            }
            //WE MOVING
            else if (!collider.CompareTag("Stopper"))
            {
                //Wall BUFFER
                if (collider.name != "Goal")
                {
                    if (Vector3.Angle(rigidbody.velocity.normalized, moveVector.normalized) > dampingAngle)
                    {
                        rigidbody.velocity = rigidbody.velocity * (1 - dampingSpeed);
                    }
                    rigidbody.AddForce(moveVector.normalized * grappleForce);
                }
                else
                {
                    goal.GetComponent<goal>().NextLevel();
                }
            }
            else
            {
                grappleHead.StopGrappling();
            }
            yield return new WaitForSeconds(.02f);
        }
    }

    public void StopGrappling()
    {
        if (grappleCoroutine != null)
        {
            StopCoroutine(grappleCoroutine);
        }
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
}
