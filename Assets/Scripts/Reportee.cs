using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Reportee : MonoBehaviour
{
    public List<Reporter> minions;
    Reporter myReporter;
    public bool allFixed;
    public string fixedString;
    [SerializeField] private GameObject fixedText;

    //This is a boolean used to control whether a Reportee subclass will react to a report by doing a physical task.
    //For example, Door objects use this when opening
    public bool react;
    // Start is called before the first frame update

    public List<Action> actions;
    public virtual void Start()
    {
        myReporter = GetComponent<Reporter>();
        fixedText.SetActive(false);
        Debug.Log(myReporter);
        foreach(Reporter rep in minions)
        {
            rep.AddReportee(this);
        }

        //Get All Actions attached to gameObject
        Action[] myActions = GetComponents<Action>();
        foreach(Action act in myActions)
        {
            actions.Add(act);
        }

    }

    public virtual void GatherReport()
    {
        Debug.Log(this.gameObject.name);
        bool isAllEqual = minions.TrueForAll(x => x.GetFixed().Equals(minions[0].GetFixed())) && minions[0].GetFixed() == true;
        if (isAllEqual)
        {
            StartCoroutine(FixedText());
            react = true;
            allFixed = true;
            Debug.Log(fixedString);
            foreach (Reporter rep in minions)
            {
                rep.GetComponent<SaveAndLoad>().Save();
            }
            if (myReporter != null)
            {
                myReporter.Fix();
            }
            GetComponent<SaveAndLoad>().Save();

            //Run Actions
            RunActions();
        }
    }

    private IEnumerator FixedText()
    {
        fixedText.SetActive(true);
        //without empty override
        if (fixedString != ""){ //dont override if you have no new text to add
            fixedText.GetComponent<TextMeshProUGUI>().text = fixedString;
        }
        //with empty override:
            //fixedText.GetComponent<TextMeshProUGUI>().text = fixedString;
        yield return new WaitForSeconds(3f);
        fixedText.SetActive(false);
    }

    private void RunActions()
    {
        foreach (Action act in actions)
        {
            act.Triggered();
        }
    }

}
