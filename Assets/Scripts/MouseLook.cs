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

    //PLAYER REFERENCE
    //reference to player object
    public Transform playerBody;
    //MOVE PLAYER CONTROLLER (COLLIDER)
    //public CharacterController controller;
    public LayerMask Pizza;

    //FOR ANIMATION (BLINKING)
    [SerializeField] private GameObject cinematicBarsContainerGO;
    //[SerializeField] private Animator cinematicBarsAnimator;
    //public GameObject hand;
    private GameObject hand;
    float timer = -1.0f;

    [SerializeField] private Rigidbody rigidbody;

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

        //print("I love to eat pizza");

        //KILL BLINK IF IT PLAYS AT THE START
        hand = GameObject.Find("CinematicBlackBarsContainer");
        //print(hand);
        if (hand != null){
            hand.SetActive(false);
        }
        //create mask for walls
        //LayerMask Pizza = LayerMask.GetMask("Pizza");

        //GET SPHERE RENDERER (cube crosshair)
        cubeRenderer = cube.GetComponent<Renderer>();
        

        //GET GOAL Renderer
        goalRenderer = goal.GetComponent<Renderer>();

        //GET AUDIO SOURCE
        source = GetComponent<AudioSource>();

        //SET CAMERA TO LOOK CORRECTLY
        //transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        //playerBody.rotation = Quaternion.Euler(0f, 90f, 0f);
        //print(playerBody.rotation);
        //print(transform.rotation);
        //transform.rotation = Quaternion.identity;
        //transform.rotation = new Quaternion(0f, 0.7f, 0f, 0.7f);
        //transform.rotation = Quaternion.Euler(90f, 90f, 0f);
        //transform.rotation = Quaternion.Euler(new Vector3(0,10,0));

        //transform.normal

    }
    /*
    void OnGUI()
    {
        //GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "This is a box");
        //GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "This is a box"+ Mathf.Round(Time.timeSinceLevelLoad));
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "This is a box: "+ Mathf.Round(Time.timeSinceLevelLoad * 10)/10f);
    }*/

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
        yRotation += mouseX;
        //clamp stuff
        //THIS IS OG
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        //THIS IS TEXT EXPERIMENT
        //xRotation = Mathf.Clamp(xRotation, -180f, 0f);

        //BY THE WAY YOU ARE USING ABSOLUTE COORDINATES FOR ROTATION, NOT RELATIVE
        //depending on the new surface you are atteched to, you should change
        //transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

        //MOVE CAMERA
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, zRotation);
        //print(transform.localRotation);
        //transform.localRotation = Quaternion.Euler(90f, xRotation, 0f);
        //print(xRotation+"asfd"+yRotation);
        //print(yRotation);


        //print(transform.rotation);
        //Input.ResetInputAxes()
        //transform.rotation = Quaternion.Euler(0f, 90f, 0f);

        





        //RAYCAST/ TELEPORTATION
        RaycastHit hit;

        //if (Physics.Raycast(transform.position, transform.forward, 100f, Pizza))
        //if (Physics.Raycast(transform.position, transform.forward, out hit, 100f, Pizza))
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
                //make cube grow based on the distance to the player
                //cube.transform.localScale = 4*(Vector3.one);
                //print(newVector.magnitude);
                cube.transform.localScale = newVector.magnitude * (Vector3.one)/16;

                //CHANGE CUBE COLOR
                //cubeRenderer.material.SetColor("_Color", new Color(0f, 0f, 0f));
                //cubeRenderer.enabled = true;

                //hit the goal


                /*
                if (hit.collider.CompareTag("Goal")){
                    cubeRenderer.material.SetColor("_Color", new Color(1f, 1f, 1f));
                }*/

            }

            //DEBUGGGING FOR CAMERA REVERSAL
            Debug.DrawRay(hit.transform.position,hit.normal*50f);

            //Vector3 relativePos = transform.position - hit.transform.position;

            Vector3 newRight = Vector3.Cross(-hit.normal, transform.forward);

            Debug.DrawRay(newRight, newRight*50f);
            //Debug.DrawRay(transform.position, -hit.transform.position);



            // the second argument, upwards, defaults to Vector3.up
            //Quaternion rotation = Quaternion.LookRotation(relativePos, hit.normal);
            //transform.rotation = rotation;


            //if (Input.GetKey(KeyCode.Mouse0)){
            if (Input.GetKey(KeyCode.Mouse0))
            {
                //if (Input.GetButton(KeyCode.Mouse1)){


                if (hit.collider.CompareTag("MoveableObject"))
                {
                    hit.collider.GetComponent<Rigidbody>().AddForceAtPosition(newVector.normalized * -10f, hit.point);
                    rigidbody.AddForce(newVector.normalized * 10f);
                }
                //WE MOVING
                else if (!hit.collider.CompareTag("Stopper"))
                {

                    timer = 0.0f;
                    //ShowBars();

                    /*Debug.Log("FIRE");
                    Debug.Log(hit.collider.name);*/


                    //Wall BUFFER
                    if (hit.collider.name != "Goal")
                    {

                        rigidbody.AddForce(newVector.normalized * 10f);

                        /*
                        Vector3 relativePos = transform.position - hit.transform.position;

                        // the second argument, upwards, defaults to Vector3.up
                        Quaternion rotation = Quaternion.LookRotation(relativePos, hit.normal);
                        transform.rotation = rotation;
                        */

                        //PLAY SOUND
                        source.Play();

                        //transform.localRotation = Quaternion.Euler(0f, 0f, 180f);

                        //transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
                        //transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    }
                    else {//if we hit the goal

                        //try to play other song, maybe put it in the other object
                        goal.GetComponent<goal>().NextLevel();
                    }
                    //controller.Move(newVector);
                    //transform.position += newVector;




                    //yield return new WaitForSeconds(0.6f);
                    //hand = GameObject.Find("CinematicBlackBarsContainer");
                    //hand.SetActive(false);

                }
                else
                {

                    Debug.Log("Hit stopper");
                }


                //transform.position = hit.point*0.5f;
                //print(hit.collider.tag);
                //print(hit.collider.tag);
                /*
                if (hit.collider.tag == "Pizza")
                {
                    Debug.Log("I hit a pizza");
                    //Debug.Log("Hit ground");
                }*/




                //transform.position = hit.point*0.5f;
                //transform.position += newVector*0.1f;

                //MOVE PLAYER
                //playerBody.position += newVector*0.1f;
                //MOVE PLAYER WITH UNIT VECTOR
                //controller.Move(newVector.normalized * 10f * Time.deltaTime);
                //move by moving character
                //playerBody.position += newVector.normalized * 0.1f * Time.deltaTime;
                //moving by moving the controller (makes collisions work)
                //MOVE PLAYER HOLE VECTOR DISTANCE

                //MADE BY TRAVIS
                //playerBody.position = hit.point - newVector.normalized * 10f;

                //float dist = Vector3.Distance(hit.point, transform.position);
                //EQUATION NORMALLY BE. UNIT VECTOR = VECTOR/DISTANCE OF VECTOR
                //print("Distance to other: " + dist);
                //playerBody.position += newVector*0.1f;
            }
            else { //RIGHT CLICK NOT DONE
                //SET CUBE COLOR

                //CHANGE CUBE COLOR
                //cubeRenderer.material.SetColor("_Color", new Color(0f, 0f, 0f));
                //cubeRenderer.enabled = true;

                //DOINT IT THIS WAY IS VERY INNEFICENT BUT FIT
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
                    //cubeRenderer.material.SetColor("_Color", new Color(0f, 0f, 1f));
                    cubeRenderer.material.SetColor("_Color", new Color(1f, 1f, 1f));
                    //cubeRenderer.enabled = false;
                    cubeRenderer.enabled = true;
                }
                else { //NO IMPORTANT COLLISIONS HAPPENING
                    //SET CUBE TO RED
                    cubeRenderer.material.SetColor("_Color", new Color(1f, 0f, 0f));
                    cubeRenderer.enabled = true;

                    //SET GOAL TO ORANGE
                    goalRenderer.material.SetColor("_Color", new Color(1f, 0.318f, 0f));

                } //MAYBE SET ONE OF THESE FOR INVISBLE WALLS

                    //CHANGE CUBE COLOR
                    //cubeRenderer.material.SetColor("_Color", new Color(1f, 0f, 0f));
                    //cubeRenderer.enabled = true;

                    //SET INVISIBLE IF HIT THE GOAL
                    if (hit.collider.name == "Goal")
                    {
                        //print("never say never");
                        //newVector = newVector - newVector.normalized * 5f;
                        //newVector = newVector + hit.normal * 5f;
                        //cubeRenderer.material.SetColor("_Color", new Color(0f, 0.5f, 0.5f));
                        cubeRenderer.enabled = false;

                        //test = GetComponent<MeshRenderer>();
                        //test.enabled = false;
                        //hit.collider.gameObject.transform. ;
                        Renderer goal_mesh = hit.collider.gameObject.GetComponent<Renderer>();
                        goal_mesh.material.SetColor("_Color", new Color(0f, 0.5f, 0.5f));

                    }
                //}
                //Debug.Log("No collider hit");
            }
            /*
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                print("asasdfdddf");
                hand = GameObject.Find("CinematicBlackBarsContainer");
                //CinematicBlackBarsContainer
                //Destroy(cinematicBarsContainerGO);
                //Destroy(hand);
                hand.SetActive(false);
            }*/
            //Debug.Log("mom it happened, get the cameara");
            //Debug.DrawLine(transform.position - Vector3.up / 2, transform.forward * 20f, Color.blue);
            //Debug.DrawLine(transform.position  - Vector3.up / 2, transform.forward * 20f, Color.blue);

            //raycasts work well, however, the problem arises that you will teleport even if you have an object that is not part of the Pizza layer (if there is a non-grappable block between an actual grappable one and the player)
        }
        else //IF RAYCAST DIDN'T HIT
        {
            //Debug.DrawLine(transform.position - Vector3.up / 2, transform.forward * 20f, Color.red);

            //CHANGE CROSSHAIR COLOR
        }
        //Physics.Raycast(transform.position, transform.forward, hit, length)

        //BLINKING TIMER
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




        //DRAW LINE
        //Debug.DrawLine(Vector3.zero, transform.forward*100f, Color.blue);


        //QUIT GAME

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        //RESET ROOM
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }




        #region //OLD CODE
        //playerBody.
        /*
         *TURN ON IF YOU WANT PLAYER TO BE ABLE TO MOVE
        //JUMP
        //if (Input.GetKeyDown(KeyCode.Space))
        if (Input.GetKey(KeyCode.Space))
        {
            playerBody.position += new Vector3(0f,20f,0f)*Time.deltaTime;
            //transform.position += new Vector3(0f,1f,0f);
        }

        //"CROUCH"
        if (Input.GetKey(KeyCode.C))
        //if (Input.GetKey(KeyCode.LeftShift))
        {
            playerBody.position += new Vector3(0f, -20f, 0f) * Time.deltaTime;
            //transform.position += new Vector3(0f,1f,0f);
        }
        */

        /*
        //RAYCAST V2
        RaycastHit hitPoint;
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //if (Physics.Raycast(ray, out hitPoint, Mathf.Infinity)) //this doesn't work for some reason, I will add previous code
        if (Physics.Raycast(transform.position, transform.forward, out hitPoint, Mathf.Infinity))
        {
            if (hitPoint.collider.tag == "Pizza")
            {
                Debug.Log("Hit ground");
                Debug.Log("Hit ground");
            }

            if (hitPoint.collider.tag == "Default")
            {
                Debug.Log("Hit object");
            }
        }
        else
        {
            Debug.Log("No collider hit");
        }*/

        //Debug.Log(mouseX);
        

        //ROTATE ON X
        //transform.Rotate(0.0f, mouseX, 0.0f, Space.Self);
        //ROTATE ON Y
        //transform.Rotate(-mouseY, 0.0f, 0.0f, Space.Self);
        //Debug.Log("I LOVE TRAINS");
        //THIS DONT WORK AS WELL CAUSE DOES Z ROTATION
        //look up i swear
        //playerBody.Rotate(Vector3.up * mouseX);

        //MOVE CAMERAf
        //ROTATE THE PLAYER OBJECT
        //YOU BELOW ARE MY PROBLEM
        //playerBody.Rotate(Vector3.up * mouseX);
        //DONT UNDERSTAND REALLY, BUT FUCKIT WE GAMING
        //END OF BRACKEYS CODE
        #endregion 

    }
}
