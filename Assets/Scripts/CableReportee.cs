using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableReportee : Reportee
{
    // Start is called before the first frame update
    //[SerializeField] private Vector3 openPos;
    [SerializeField] private LightCables cables;
    //private Vector3 closedPos;
    //float speed = 0.5f;
    //bool open = false;

    /*
    public override void Start()
    {
        closedPos = new Vector3(transform.localPosition.x,
            transform.localPosition.y,
            transform.localPosition.z);
        base.Start();
    }*/

    void Update()
    {
        if (allFixed)
        {
            //transform.localPosition = Vector3.Lerp(transform.localPosition, openPos, speed * Time.deltaTime);
            //call lightswitch
            cables.FlipSwitch(true);
        }
    }
}
