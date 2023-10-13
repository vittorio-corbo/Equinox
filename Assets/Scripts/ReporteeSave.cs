using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReporteeSave : SaveAndLoad
{
    private Reportee script;
    private bool allFixed;
    protected override void Awake()
    {
        script = GetComponent<Reportee>();
        if (script == null)
        {
            gameObject.AddComponent<SaveAndLoad>();
            GameObject.Destroy(this);
        }
    }

    public override void Save()
    {
        allFixed = script.allFixed;
        base.Save();
    }

    public override void Load()
    {
        script.allFixed = allFixed;
        base.Load();
    }
}
