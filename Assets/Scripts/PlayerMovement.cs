using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //reference to player object
    public CharacterController controller;



    //how much we zooming (PLAYER VELOCITY)
    public float speed = 12f;

    //GRAVITY
    public float gravity = -9.8f;
    Vector3 velocity;

    // Start is called before the first frame update
    /*
    //dont need you
    void Start()
    {
        
    }*/

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //it x and z cause y is up in this case
        Vector3 move = transform.right * x + transform.forward * z;
        //PLAYER MOVEMENT
        //TURN ON IF YOU WANT PLAYER TO MOVE
        //controller.Move(move * speed * Time.deltaTime);

        //GRAVITY
        //y = 0.5*g*t^2

        //velocity.y += gravity * Time.deltaTime;
        //controller.Move(velocity * Time.deltaTime);


    }
}
