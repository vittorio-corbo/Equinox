using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SaveAndLoad : MonoBehaviour
{
    private Vector3 position;
    private Quaternion rotation;
    private Vector3 velocity;
    private Vector3 angularVelocity;


    protected virtual void Awake()
    {
        if (GetComponent<NeedExternalObject>() != null)
        {
            gameObject.AddComponent<FixableObjectSave>();
            Destroy(this);
        }
        if (GetComponent<FixingObject>() != null)
        {
            gameObject.AddComponent<FixingObjectSave>();
            Destroy(this);
        }
        if (GetComponent<GrappleHead>() != null)
        {
            gameObject.AddComponent<GrappleSave>();
            Destroy(this);
        }
        if (GetComponent<Reportee>() != null)
        {
            gameObject.AddComponent<ReporteeSave>();
            Destroy(this);
        }
        if (GetComponent<PlayerGrapple>() != null)
        {
            gameObject.AddComponent<PlayerSave>();
            Destroy(this);
        }
        if (GetComponent<Rigidbody>() == null)
        {
            Destroy(this);
        }
    }
    public virtual void Save()
    {
        position = transform.localPosition;
        rotation = transform.localRotation;
        if (GetComponent<Rigidbody>() != null)
        {
            velocity = GetComponent<Rigidbody>().velocity;
            angularVelocity = GetComponent<Rigidbody>().angularVelocity;
        }
        Debug.Log("Saved " + gameObject.name);
    }

    public virtual void Load()
    {
        transform.localPosition = position;
        transform.localRotation = rotation;
        if (GetComponent<Rigidbody>() != null)
        {
            GetComponent<Rigidbody>().velocity = velocity;
            GetComponent<Rigidbody>().angularVelocity = angularVelocity;
        }
        Debug.Log(gameObject.name + position);
    }
}
