using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class isButtonOnTwo : MonoBehaviour
{
    [SerializeField] LightCables lightcables;
    [SerializeField] Reportee reporteePre;
    [SerializeField] ButtonScript buttonScript;
    [SerializeField] Reportee reportee;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    //could/should've done this with an action reporter which also checks these other things?
    //no this would not have worked lol


    // Update is called once per frame
    void Update()
    {
        if (buttonScript.buttonDown && reportee.allFixed && reporteePre.allFixed){
            lightcables.FlipSwitch(true);

        }else{
            lightcables.FlipSwitch(false);
        }
        
    }
}
