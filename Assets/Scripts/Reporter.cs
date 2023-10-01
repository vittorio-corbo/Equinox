using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reporter : MonoBehaviour
{
    List<Reportee> reportees = new List<Reportee>();
    Reporter selfReportee;
    public bool isFixed = false;
    public  void Fix()
    {
        isFixed = true;
        foreach (Reportee reportee in reportees)
        {
            reportee.GatherReport();
        }
    }

    public void AddReportee(Reportee rep) 
    {
        reportees.Add(rep);
    }

    public bool GetFixed()
    {
        return isFixed;
    }
}
