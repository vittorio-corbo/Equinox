using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSave : SaveAndLoad
{
    private PlayerGrapple script;
    public Vector3 areaPosition;
    public Quaternion areaRotation;

    public void SetArea(Vector3 pos, Quaternion rot)
    {
        areaPosition = pos;
        areaRotation = rot;
    }

    public void Awake()
    {
        script = GetComponent<PlayerGrapple>();
        if (script == null)
        {
            gameObject.AddComponent<SaveAndLoad>();
            GameObject.Destroy(this);
        }
    }

    public override void Save()
    {
        areaPosition = transform.position;
        areaRotation = transform.rotation;
        base.Save();
    }

    public override void Load()
    {
        base.Load();
        transform.position = areaPosition;
        transform.rotation = areaRotation;
    }
}
