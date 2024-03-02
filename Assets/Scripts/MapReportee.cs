using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapReportee : Reportee
{
    public Color unfixedColor;
    public Color semiFixedColor;
    public Color fixedColor;
    SpriteRenderer mapImage;

    public override void Start()
    {
        mapImage = GetComponent<SpriteRenderer>();
        base.Start();
    }
    public override void GatherReport()
    {
        Debug.Log(this.gameObject.name);
        bool isAllEqual = minions.TrueForAll(x => x.GetFixed().Equals(minions[0].GetFixed())) && minions[0].GetFixed() == true;
        bool oneIsEqual = false;
        foreach (Reporter rep in minions)
        {
            oneIsEqual = oneIsEqual || rep.GetFixed();
        }
        if (isAllEqual)
        {
            mapImage.color = fixedColor;
        }
        else if (oneIsEqual)
        {
            mapImage.color = semiFixedColor;
        }
        else
        {
            mapImage.color = unfixedColor;
        }
    }
}
