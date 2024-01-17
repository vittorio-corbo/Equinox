using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCables : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Material litMaterial;
    [SerializeField] private Material unlitMaterial;

    //Realize that this was not necesary to save at the moment
    //public bool state;
    public void Start()
    {
        //Set to unlit color
        Material newShade = unlitMaterial;

        //Turn to new colour
        foreach (Transform child in transform)
        {
            child.transform.GetComponent<MeshRenderer>().material = newShade;
        }
    }

    void Update()
    {
        /*
        if (allFixed)
        {
            //get children and change their materials
            foreach (Transform child in transform)
            {
                //child is your child transform
                //child.transform.GetComponent<MeshRenderer>().materials[0].mainTexture = litMaterial.mainTexture;
                child.transform.GetComponent<MeshRenderer>().material.mainTexture = litMaterial.mainTexture;
                //grappleGun.transform.GetChild(0).GetComponent<MeshRenderer>().materials[0].mainTexture = grab.mainTexture;
            }

            //Destroy(gameObject);
            
        }*/
    }

    //public void LightSwitch(bool turnOn)
    public void FlipSwitch(bool newState)
    {
        //Set new color
        Material newShade;
        if (newState)
        {
            newShade = litMaterial;
        }else{
            newShade = unlitMaterial;
        }

        //Turn to new colour
        foreach (Transform child in transform)
        {
            //child is your child transform
            //child.transform.GetComponent<MeshRenderer>().materials[0].mainTexture = litMaterial.mainTexture;

            //OLD AND WORKS
                //child.transform.GetComponent<MeshRenderer>().material.mainTexture = newShade.mainTexture;
            //NEW ATTEMPTS
            child.transform.GetComponent<MeshRenderer>().material = newShade;


            //grappleGun.transform.GetChild(0).GetComponent<MeshRenderer>().materials[0].mainTexture = grab.mainTexture;
        }

    }
}
