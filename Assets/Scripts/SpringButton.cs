using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringButton : ButtonScript
{
    [SerializeField] SpringScript spring;

    public new void Start()
    {
        spring.winching = true;
        base.Start();
    }

    public override void ButtonOn()
    {
        spring.winching = false;
    }

    public override void ButtonOff()
    {
        spring.winching = true;
    }
}
