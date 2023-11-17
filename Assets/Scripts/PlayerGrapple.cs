using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
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

    public GameObject grappleGun;

    //Hiding the camera's rigidbody with the one we want
    [SerializeField] private new Rigidbody rigidbody;

    //public float MAXDISTANCE;
    //public float MAXCUBEDIST;

    
    public Laser crc;
    //Note: This is used both when the point you are looking at is out of range and when you are already grappling
    

    //GOAL REFERENCE
    public GameObject goal;
    private Renderer goalRenderer;

    public float grappleForce;

    //AUDIO
    private AudioSource source;
    public AudioClip hold;
    public AudioClip stopHold;
    private bool musicPlaying;

    private GrappleHead grappleHead;
    private Transform grappleHeadTransform;
    

    private float normalGrappleDist;

    private bool holding;

    private Transform playerCamera;
    private Vector3 endPoint;

    [SerializeField] float dampingAngle;
    [SerializeField] float dampingSpeed;

    //GLASS MATERIAL
    //public Material glass;

    //GRAVITY/CAMERA EXPERIMENT

    //Experimenting with cube rendering

    public void ShowBars() {
        cinematicBarsContainerGO.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        normalGrappleDist = crc.MAXDISTANCE;
        //KILL BLINK IF IT PLAYS AT THE START
        hand = GameObject.Find("CinematicBlackBarsContainer");
        //print(hand);
        if (hand != null){
            hand.SetActive(false);
        }

        //GET GOAL Renderer
        goalRenderer = goal.GetComponent<Renderer>();

        //GET AUDIO SOURCE
        source = GetComponent<AudioSource>();

        //Use FindObjectsOfTypeAll so it finds inactive scripts too
        grappleHead = Resources.FindObjectsOfTypeAll(typeof(GrappleHead))[0] as GrappleHead;
        grappleHeadTransform = grappleGun.transform.GetChild(1);
        holding = Input.GetKeyDown(KeyCode.F);
        playerCamera = transform.GetChild(0);
        foreach (SaveAndLoad save in Resources.FindObjectsOfTypeAll<SaveAndLoad>())
        {
            save.Save();
        }
    }

    // Update is called once per frame
    //MOVE DA CAMERA
    void Update()
    {
        if (musicPlaying && PauseScript.isPaused || (!musicPlaying) && !(PauseScript.isPaused))
        {
            PauseUnpauseSFX();
        }
        if (!(PauseScript.isPaused))
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Debug.Log("loading");
                foreach (SaveAndLoad go in Resources.FindObjectsOfTypeAll(typeof(SaveAndLoad)))
                {
                    go.Load();
                }
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                ToggleHold();
            }
            endPoint = playerCamera.transform.position + crc.MAXDISTANCE * playerCamera.transform.forward.normalized;
            //If not holding to grapple, treat as a toggle
            if (!MenuActions.holdToGrapple)
            {

                if (Input.GetMouseButtonDown(0))
                {
                    //PLAY SOUND
                    //source.Play();
                    crc.outOfRange = true;
                    if (crc.hitSomething)
                    {
                        grappleHead.StartMovement(grappleHeadTransform.position, (crc.hit.point - grappleHeadTransform.position).normalized);
                    }
                    else
                    {
                        grappleHead.StartMovement(grappleHeadTransform.position, (endPoint - grappleHeadTransform.position).normalized);
                    }
                }
            } else
            {
                if (Input.GetMouseButtonDown(0) && crc.shooting == false)
                {
                    crc.outOfRange = true;
                    if (crc.hitSomething)
                    {
                        grappleHead.StartMovement(grappleHeadTransform.position, (crc.hit.point - grappleHeadTransform.position).normalized);
                    }
                    else
                    {
                        grappleHead.StartMovement(grappleHeadTransform.position, (endPoint - grappleHeadTransform.position).normalized);
                    }
                }
                if (Input.GetMouseButtonUp(0) && crc.shooting == true)
                {
                    crc.outOfRange = true;
                    if (crc.hitSomething)
                    {
                        grappleHead.StartMovement(grappleHeadTransform.position, (crc.hit.point - grappleHeadTransform.position).normalized);
                    }
                    else
                    {
                        grappleHead.StartMovement(grappleHeadTransform.position, (endPoint - grappleHeadTransform.position).normalized);
                    }
                }
            }


            if (timer != -1.0f)
            {
                timer += Time.deltaTime;
                // int seconds = timer % 60;
                //print(timer);
                if (timer >= 0.20f)
                {
                    hand = GameObject.Find("CinematicBlackBarsContainer");
                    //print(hand);
                    if (hand != null)
                    {
                        hand.SetActive(false);
                    }
                }
            }
        }
    }

    private void ToggleHold()
    {
        if (GetComponent<ConfigurableJoint>() == null)
        {
            HoldSurface();
        }
        else
        {
            StopHolding();
        }
    }

    private void HoldSurface() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 5f)) //not check for layer anymore
        {
            if (hit.transform.gameObject.GetComponent<Rigidbody>() != null)
            {
                Debug.Log(hit.transform.gameObject);
                ConfigurableJoint joint = gameObject.AddComponent<ConfigurableJoint>();
                joint.connectedBody = hit.transform.gameObject.GetComponent<Rigidbody>();
                joint.xMotion = ConfigurableJointMotion.Locked;
                joint.yMotion = ConfigurableJointMotion.Locked;
                joint.zMotion = ConfigurableJointMotion.Locked;
                if (hit.transform.gameObject.GetComponent<GrappleExtend>() != null)
                {
                    hit.transform.gameObject.GetComponent<GrappleExtend>().ExtendGrapple();
                }
                if (hit.transform.gameObject.GetComponent<Rocket>() != null)
                {
                    hit.transform.gameObject.GetComponent<Rocket>().StartMovement();
                }
                PlaySFX(hold, false);
            }
        }
    }

    public void StopHolding()
    {
        Destroy(GetComponent<ConfigurableJoint>());
        crc.MAXDISTANCE = normalGrappleDist;
        PlaySFX(stopHold, false);
    }

    

    public void StartGrappling(Collider collider)
    {
        if (grappleCoroutine != null)
        {
            StopCoroutine(grappleCoroutine);
        }
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
                    rigidbody.velocity = rigidbody.velocity * (1-dampingSpeed);
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

    private void OnJointBreak(float breakForce)
    {
        StopHolding();
    }

    private void PauseUnpauseSFX()
    {
        if (musicPlaying)
        {
            source.Pause();
        }
        else
        {
            source.UnPause();
        }
        musicPlaying = !musicPlaying;
    }

    //Plays the sound, prints stack trace to console if it cannot find the file
    private void PlaySFX(AudioClip clip, bool loop)
    {
        try
        {
            source.clip = clip;
            if (loop)
            {
                source.loop = true;
            }
            else
            {
                source.loop = false;
            }
            source.volume = MenuActions.effectsVolume;
            source.Play();
            musicPlaying = true;
        }
        catch (System.Exception e)
        {
            Debug.LogException(e);
        }
    }
}
