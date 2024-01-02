using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


//Make this abstract
public class ActionLightCables : Action
{
    //[SerializeField] public Reportee reportee;

    [SerializeField] public LightCables lightcable;
    
    public override void Triggered()
    {
        //if other dude is fixed : allFixed == true;
        //if (reportee.allFixed){
            lightcable.FlipSwitch(true);
        //}
        //throw new System.NotImplementedException();
    }

    //FUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUCK
    //THIS DOES NOT WORK IF IT ISN'T TRIGGERED
    //wait. maybe this works if i have a pyramid to this
    //     x
    //    / \
    //   x
    // /  \
    //reporter reportee system is lightweight
    //what i should do is create an object that handles all of this and will have a chain of reporters and reportees

    //Do other shit
}
