using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixableObjectSave : SaveAndLoad
{
    private NeedExternalObject script;
    private List<GameObject> connectedObjects;

    private List<NeedExternalObject.FixableObjectCheck> checks;

    protected override void Awake()
    {
        script = GetComponent<NeedExternalObject>();
        if (script == null)
        {
            GameObject.Destroy(this);
        }
    }

    public override void Save()
    {
        connectedObjects = new List<GameObject>(script.connectedObjects);
        checks = new List<NeedExternalObject.FixableObjectCheck>();
        foreach (NeedExternalObject.FixableObjectCheck check in script.fixableObjectChecks)
        {
            checks.Add(new NeedExternalObject.FixableObjectCheck(check));
        }
        base.Save();
    }

    public override void Load()
    {
        foreach (GameObject obj in script.connectedObjects)
        {
            Debug.Log(obj);
            if (!connectedObjects.Contains(obj))
            {
                GameObject.Destroy(obj.GetComponent<FixedJoint>());
                if (obj.GetComponent<FixingObject>().isGrabbable)
                {
                    obj.tag = "Grabbable";
                }
            }
        }
        script.connectedObjects = new List<GameObject>(connectedObjects);
        script.fixableObjectChecks = new List<NeedExternalObject.FixableObjectCheck>();
        foreach (NeedExternalObject.FixableObjectCheck check in checks)
        {
            script.fixableObjectChecks.Add(new NeedExternalObject.FixableObjectCheck(check));
        }
        base.Load();
    }
}
