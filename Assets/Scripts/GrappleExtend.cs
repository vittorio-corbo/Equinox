using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleExtend : MonoBehaviour
{
    [SerializeField] private float GrappleDist;

    public void ExtendGrapple()
    {
        FindObjectOfType<PlayerGrapple>().crc.MAXDISTANCE = GrappleDist;
    }
}
