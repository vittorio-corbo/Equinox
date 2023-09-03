using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using static UnityEngine.GraphicsBuffer;

public class MouseLook : MonoBehaviour
{
    //MOUSE INPUTS
    public float mouseSensitivity = 200f;
    private Coroutine runningCoroutine;
    float xRotation = 0f;
    float yRotation = 0f;
    float zRotation = 0f;

    
    public Transform playerBody;
    public LayerMask Pizza;

    //FOR ANIMATION (BLINKING)
    [SerializeField] private GameObject cinematicBarsContainerGO;
    private GameObject hand;
    float timer = -1.0f;

    //Hiding the camera's rigidbody with the one we want
    [SerializeField] private new Rigidbody rigidbody;

    //CROSSHAIR
    public GameObject cube;
    private Renderer cubeRenderer;

    //GOAL REFERENCE
    public GameObject goal;
    private Renderer goalRenderer;

    //AUDIO
    private AudioSource source;

    private Vector3 momentum;

    //GLASS MATERIAL
    //public Material glass;

    //GRAVITY/CAMERA EXPERIMENT
    

    public void ShowBars() {
        cinematicBarsContainerGO.SetActive(true);
        Debug.Log("asdf");
    }

    // Start is called before the first frame update
    void Start()
    {
        //lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


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

    }

    // Update is called once per frame
    //MOVE DA CAMERA
    void Update()
    {
        
        //MOUSE/ LOOKING CONTROLS
        //these values are relative to the mouse movemnt (aka, how much mouse move in x and y axis)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        //Debug.Log(mouseX);
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        //print(Input.GetAxis("Mouse Y"));

        //CHEEKY FORCE ROTATES
        if (Input.GetKey(KeyCode.LeftArrow)){yRotation -= 0.5f;}
        if (Input.GetKey(KeyCode.RightArrow)){yRotation += 0.5f;}
        if (Input.GetKey(KeyCode.UpArrow)) { xRotation -= 0.5f; }
        if (Input.GetKey(KeyCode.DownArrow)) { xRotation += 0.5f; }
        if (Input.GetKey(KeyCode.RightShift)) { zRotation -= 0.5f; }
        if (Input.GetKey(KeyCode.Backslash)) { zRotation += 0.5f; }


        //ROTATE CAMERA
        //BRACKEYS CODE

        xRotation -= mouseY;
        if (Input.GetMouseButton(1))
        {
            zRotation -= mouseX;
        }
        else
        {
            yRotation += mouseX;
        }

        //MOVE CAMERA
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, zRotation);

        





        //RAYCAST/ TELEPORTATION
        RaycastHit hit;
        //used to be 100
        if (Physics.Raycast(transform.position, transform.forward, out hit, 5000f)) //not check for layer anymore
        {
            //HOLDS THE DISTANCE
            Vector3 newVector = hit.point - transform.position;

            //CUBE CROSSHAIR
            if (!hit.collider.CompareTag("CUBE")){//IGNORE SELF (cube)
                //SET CUBE POSITION
                cube.transform.position = hit.point;

                //SET CUBE SCALE (based on distance)
                cube.transform.localScale = newVector.magnitude * (Vector3.one)/16;
            }
            if (Input.GetMouseButton(0))
            {


                if (hit.collider.CompareTag("MoveableObject"))
                {
                    hit.collider.GetComponent<Rigidbody>().AddForceAtPosition(newVector.normalized * -5, hit.point);
                    rigidbody.AddForce(newVector.normalized * 5);
                }
                //WE MOVING
                else if (!hit.collider.CompareTag("Stopper"))
                {


                    //Wall BUFFER
                    if (hit.collider.name != "Goal")
                    {

                        rigidbody.AddForce(newVector.normalized * 10f);

                        //PLAY SOUND
                        source.Play();
                    }
                    else {
                        goal.GetComponent<goal>().NextLevel();
                    }

                }
                else
                {

                    Debug.Log("Hit stopper");
                }
            }
            else {

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

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        //RESET ROOM
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
}
