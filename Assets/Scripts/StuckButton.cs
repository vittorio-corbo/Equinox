using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckButton : Button
{
    public void Update()
    {
        if (buttonScript != null){
            buttonScript.buttonDown = true;
        }

    }
}
