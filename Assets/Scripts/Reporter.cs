using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reporter : MonoBehaviour
{
    List<Reportee> reportees = new List<Reportee>();
    Reporter selfReportee;
    public bool isFixed = false;
    protected void Fix()
    {
        isFixed = true;
        foreach (Reportee reportee in reportees)
        {
            reportee.Report();
        }
    }
}
