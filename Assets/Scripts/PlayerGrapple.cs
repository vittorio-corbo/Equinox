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

    //GOAL REFERENCE
    public GameObject goal;
    private Renderer goalRenderer;

    public float grappleForce;

    //AUDIO
    private AudioSource source;

    private GrappleHead grappleHead;

    private Vector3 momentum;

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
    }

    // Update is called once per frame
    //MOVE DA CAMERA
    void Update()
    {
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            //PLAY SOUND
            source.Play();
            grappleHead.StartMovement(transform.position, transform.forward);
        }
        //used to be 100
        if (Physics.Raycast(transform.position, transform.forward, out hit, MAXDISTANCE)) //not check for layer anymore
        {
            //HOLDS THE DISTANCE
            Vector3 newVector = hit.point - transform.position;
            cubeRenderer.enabled = true;
            cubeRenderer.material.SetColor("_Color", new Color(1f, 0f, 0f));

            //CUBE CROSSHAIR
            if (!hit.collider.CompareTag("CUBE") && hit.collider.GetComponent<GrappleHead>() == null)
            {//IGNORE SELF AND GRAPPLE HEAD
                //SET CUBE POSITION
                cube.transform.position = hit.point;

                //SET CUBE SCALE (based on distance)
                cube.transform.localScale = newVector.magnitude * (Vector3.one)/16;
            }
            else { // I think this may be a glitch, as these if statements are unreachable. Can someone back this up?

                if (hit.collider.CompareTag("Stopper"))
                {
                    //SET TO BLACK
                    cubeRenderer.material.SetColor("_Color", new Color(0f, 0f, 0f));
                    cubeRenderer.enabled = true;
                }
                else if (hit.collider.name == "Goal")//HIT THE GOAL
                {
                    //SET CUBE INVISBLE
                    cubeRenderer.enabled = false;

                    //SET THE GOAL TO BLUE
                    goalRenderer.material.SetColor("_Color", new Color(0f, 0f, 1f));
                }
                else if (hit.collider.CompareTag("Glass")) { //IT IS GLASS
                    //CHANGE COLOR
                    cubeRenderer.material.SetColor("_Color", new Color(1f, 1f, 1f));
                    cubeRenderer.enabled = true;
                }
                else { //NO IMPORTANT COLLISIONS HAPPENING
                    //SET CUBE TO RED
                    cubeRenderer.material.SetColor("_Color", new Color(1f, 0f, 0f));
                    cubeRenderer.enabled = true;

                    //SET GOAL TO ORANGE
                    goalRenderer.material.SetColor("_Color", new Color(1f, 0.318f, 0f));

                }
                if (hit.collider.name == "Goal")
                {
                    cubeRenderer.enabled = false;
                    Renderer goal_mesh = hit.collider.gameObject.GetComponent<Renderer>();
                    goal_mesh.material.SetColor("_Color", new Color(0f, 0.5f, 0.5f));

                }
            }
        }
        //If the object is too far to grapple, turn the crosshair cube gray but keep tracking it
        else if (Physics.Raycast(transform.position, transform.forward, out hit, MAXCUBEDIST))
        {
            //HOLDS THE DISTANCE
            Vector3 newVector = hit.point - transform.position;
            cubeRenderer.enabled = true;
            cubeRenderer.material.SetColor("_Color", new Color(0.5f, 0.5f, 0.5f));

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
        }

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
}
