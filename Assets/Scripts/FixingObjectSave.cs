using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixingObjectSave : SaveAndLoad
{
    FixingObject script;
    private new void Awake()
    {
        script = GetComponent<FixingObject>();
        if (script == null)
        {
            gameObject.AddComponent<SaveAndLoad>();
            Destroy(this);
        }
    }
    public override void Load()
    {
        if (GetComponent<FixedJoint>() != null)
        {
            return;
        }
        base.Load();
    }
}
