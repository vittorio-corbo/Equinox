using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixingObject : MonoBehaviour
{
    public enum FixingObjectType
    {
        Battery,
        Motor,
        Antenna,
        Gear,
        Rock,
    }

    public FixingObjectType type;
}
