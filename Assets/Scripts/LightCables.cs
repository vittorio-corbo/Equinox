using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCables : Reportee
{
    // Start is called before the first frame update
    [SerializeField] public Material litMaterial;
    // private Vector3 closedPos;
    // float speed = 0.5f;
    // bool open = false;

    public override void Start()
    {
        //closedPos = new Vector3(transform.localPosition.x,
        //    transform.localPosition.y,
        //    transform.localPosition.z);
        base.Start();
    }

    void Update()
    {
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
            
        }
    }
}
