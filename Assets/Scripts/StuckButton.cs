using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckButton : ButtonScript
{
    private new void Update()
    {
        buttonDown = true;
        ButtonOn();
    }
}
