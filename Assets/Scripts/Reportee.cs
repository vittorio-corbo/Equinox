using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reportee : MonoBehaviour
{
    public List<Reporter> minions;
    Reporter myReporter;
    public bool allFixed;
    public string fixedString;
    // Start is called before the first frame update
    protected void Start()
    {
        myReporter = GetComponent<Reporter>();
        foreach(Reporter rep in minions)
        {
            rep.AddReportee(this);
        }
    }

    public void GatherReport()
    {
        bool isAllEqual = minions.TrueForAll(x => x.GetFixed().Equals(minions[0].GetFixed())) && minions[0].GetFixed() == true;
        if (isAllEqual)
        {
            allFixed = true;
            Debug.Log(fixedString);
            foreach (Reporter rep in minions)
            {
                rep.GetComponent<SaveAndLoad>().Save();
            }
            GetComponent<SaveAndLoad>().Save();
            if(myReporter != null)
            {
                myReporter.Fix();
            }
        }
    }

}
