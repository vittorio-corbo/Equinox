using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FixingObject;

public class NeedExternalObject : Reporter
{
    public GameObject fixedText;
    public Transform slot; // specific slot for grabbable object
    public float detectionRadius = 20f;

    [SerializeField] private List<FixingObjectType> fixingTypes;
    private List<FixableObjectCheck> fixableObjectChecks = new List<FixableObjectCheck>();
    private class FixableObjectCheck
    {
        public FixingObjectType type;
        public bool isFixed;

        public FixableObjectCheck(FixingObjectType type)
        {
            this.type = type;
            isFixed = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        fixedText.SetActive(false);
        foreach (FixingObjectType type in fixingTypes)
        {
            fixableObjectChecks.Add(new FixableObjectCheck(type));
        }

    }
    // Update is called once per frame
    void Update()
    {
        // Check if the text is active and hide it after 2 seconds
        if (fixedText.activeSelf)
        {
            StartCoroutine(HideTextAfterDelay(2f));
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.GetComponent<FixingObject>() != null)
        {
            foreach (FixableObjectCheck check in fixableObjectChecks)
            {
                if (!check.isFixed && collision.gameObject.GetComponent<FixingObject>().type == check.type)
                {
                    check.isFixed = true;
                    FixedJoint joint = collision.gameObject.AddComponent<FixedJoint>();
                    joint.connectedBody = GetComponent<Rigidbody>();
                    collision.gameObject.tag = "MoveableObject";
                    break;
                }
            }
            bool checkBool = true;
            foreach (FixableObjectCheck check in fixableObjectChecks)
            {
                checkBool = checkBool && check.isFixed;
            }
            if (checkBool && !isFixed)
            {
                Fix();
                fixedText.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.GetComponent<FixingObject>() != null)
        {
            foreach (FixableObjectCheck check in fixableObjectChecks)
            {
                if (!check.isFixed && collision.gameObject.GetComponent<FixingObject>().type == check.type)
                {
                    check.isFixed = true;
                    FixedJoint joint = collision.gameObject.AddComponent<FixedJoint>();
                    joint.connectedBody = GetComponent<Rigidbody>();
                    collision.gameObject.tag = "MoveableObject";
                    collision.GetComponent<Collider>().isTrigger = true;
                    break;
                }
            }
            bool checkBool = true;
            foreach (FixableObjectCheck check in fixableObjectChecks)
            {
                checkBool = checkBool && check.isFixed;
            }
            if (checkBool && !this.isFixed)
            {
                Fix();
                fixedText.SetActive(true);
            }
        }
    }

    private IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        fixedText.SetActive(false);
    }

    public bool getFixed()
    {
        return this.isFixed;
    }


}

