using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


//Make this abstract
public class ActionLightCables : Action
{
    [SerializeField] public LightCables lightcable;
    public override void Triggered()
    {
        lightcable.FlipSwitch(true);
        //throw new System.NotImplementedException();
    }

    //Do other shit
}
