using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleSave : SaveAndLoad
{
    private GrappleHead script;
    private Transform parent;
    private bool isActive;
    private bool retracting;
    private bool insideSomething;
    private bool colliding;

    protected override void Awake()
    {
        script = GetComponent<GrappleHead>();
        if (script == null)
        {
            gameObject.AddComponent<SaveAndLoad>();
            GameObject.Destroy(this);
        }
    }

    public override void Save()
    {

    }

    public override void Load()
    {
        gameObject.SetActive(false);
        transform.parent = null;
        script.insideSomething = false;
        GetComponent<Collider>().enabled = true;
        script.StopGrappling();
    }
}
