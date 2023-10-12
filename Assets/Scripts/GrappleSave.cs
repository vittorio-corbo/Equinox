using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleSave : SaveAndLoad
{
    private GrappleHead script;
    private Transform parent;
    private bool isActive;
    private bool retracting;

    protected override void Awake()
    {
        script = GetComponent<GrappleHead>();
        if (script == null)
        {
            GameObject.Destroy(this);
        }
    }

    public override void Save()
    {
        isActive = gameObject.activeSelf;
        parent = transform.parent;
        retracting = script.retracting;
        transform.parent = parent;
        base.Save();
    }

    public override void Load()
    {
        gameObject.SetActive(isActive);
        transform.parent = parent;
        if (parent == null)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            script.StopGrapplingNoRetract();
        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
        if (retracting)
        {
            script.StopGrappling();
        }
        base.Load();
    }
}
