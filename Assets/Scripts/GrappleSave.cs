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
        isActive = gameObject.activeSelf;
        parent = transform.parent;
        retracting = script.retracting;
        transform.parent = parent;
        insideSomething = script.insideSomething;
        colliding = GetComponent<Collider>().enabled;
        base.Save();
    }

    public override void Load()
    {
        gameObject.SetActive(isActive);
        transform.parent = parent;
        script.insideSomething = insideSomething;
        GetComponent<Collider>().enabled = colliding;
        if (parent == null)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            script.StopGrapplingNoRetract();
        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
        if (insideSomething)
        {
            script.player.StartGrappling(parent.GetComponent<Collider>());
        }
        if (retracting)
        {
            script.StopGrappling();
        }
        base.Load();
        Debug.Log(GetComponent<Rigidbody>().velocity);
    }
}
