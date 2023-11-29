using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FixingObject;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
//using UnityEngine.Random;

public class Smush : NeedExternalObject
{
    [SerializeField] Reportee areaA;
    [SerializeField] GameObject hullPieces;
    //[SerializeField] GameObject liver;

     //private new void OnTriggerEnter(Collider collision)
     protected override void OnTriggerEnter(Collider collision)
    {
        //check if item is type liver
        //if (areaA.allFixed) //if good do boring shit
        if (areaA.allFixed && Quaternion.Angle(collision.gameObject.transform.rotation, transform.rotation) < 5f) //if good do boring shit
            //maybe check rotations up top here?

        {
            
            base.OnTriggerEnter(collision);
            //TO DO: crazy fix (check angle)... fuckkkk
        }
        else
        {
            Debug.Log("colliison");
            if (collision.GetComponent<FixingObject>() != null && collision.GetComponent<FixingObject>().type == FixingObject.FixingObjectType.Liver)
            {
                Debug.Log("ham");
                //chaos
                for (int i = 0; i < hullPieces.transform.childCount; i += 1)
                {
                    if (hullPieces.transform.GetChild(i).gameObject.GetComponent<Rigidbody>() != null)
                    {
                        //hullPieces.transform.GetChild(i).gameObject.GetComponent<Rigidbody>().isKinematic = true;
                        hullPieces.transform.GetChild(i).gameObject.GetComponent<Rigidbody>().isKinematic = false;
                        hullPieces.transform.GetChild(i).gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(UnityEngine.Random.Range(0, 100), UnityEngine.Random.Range(0, 100), UnityEngine.Random.Range(0, 100)), ForceMode.Impulse);
                    }
                }
            }
        
                //turns rigibodies into not kinematic
                //random direction and force


        }
    }
}