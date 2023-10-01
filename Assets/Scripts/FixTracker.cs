using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class FixTracker : MonoBehaviour
{
    //DATA
    public bool autoUpdate = true;
    public string mechanism;
    private bool allFixed = false;
    public List<bool> mechanismParts;
    // Start is called before the first frame update
    void Start()
    {
        mechanismParts = FindObjectsInMechanism();
    }

    // Update is called once per frame
    void Update()
    {
        if(autoUpdate)
        {
            allFixed = checkAll();
        }

        if (allFixed)
        {
            Debug.Log("WOOOOOOOOOOOO YEEEEEAHHH BABYYYY");
        }
    }

    //TODO: Create multiple fixables with different mechanisms, as well as 
    //a single mechanism with multiple fixables, make sure all of this works as intended
    private List<bool> FindObjectsInMechanism()
    {
        List<bool> ret = new List<bool>();
        GameObject[] fixables = GameObject.FindGameObjectsWithTag("Fixable");
        foreach(GameObject i in fixables) {
            if(i.GetComponent<NeedExternalObject>() != null)
            {
                if (i.GetComponent<NeedExternalObject>().getMechanism() == this.mechanism)
                {
                    ret.Add(i.GetComponent<NeedExternalObject>().getFixed());
                }
            }
        }
        return ret;
    }

    private bool checkAll()
    {
        mechanismParts = FindObjectsInMechanism();
        bool allFixed = mechanismParts.TrueForAll(x => x.Equals(mechanismParts.First())) && mechanismParts.First() == true;
        return allFixed;
    }
}
