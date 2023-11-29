using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static FixingObject;

public class NeedExternalObject : Reporter
{
    //public GameObject fixedText;
    public float detectionRadius = 20f;

    public Transform[] positions;

    [SerializeField] private List<FixingObjectType> fixingTypes;
    public List<FixableObjectCheck> fixableObjectChecks = new List<FixableObjectCheck>();

    public List<GameObject> connectedObjects = new List<GameObject>();
    public class FixableObjectCheck
    {
        public FixingObjectType type;
        public bool isFixed;

        public FixableObjectCheck(FixingObjectType type)
        {
            this.type = type;
            isFixed = false;
        }

        public FixableObjectCheck(FixableObjectCheck check)
        {
            this.type = check.type;
            this.isFixed = check.isFixed;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //fixedText.SetActive(false);
        foreach (FixingObjectType type in fixingTypes)
        {
            fixableObjectChecks.Add(new FixableObjectCheck(type));
        }
        positions = new Transform[fixableObjectChecks.Count];
        try
        {
            for (int i = 0; i < fixableObjectChecks.Count; ++i)
            {
                positions[i] = transform.GetChild(i);
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("There are not enough Children Transforms on " + gameObject.name);
        }
    }
    // Update is called once per frame
    bool called = false;
    void Update()
    {
        if (isFixed && !called)
        {
            Fix();
            called = true;
        }
        // Check if the text is active and hide it after 2 seconds
        /*if (fixedText.activeSelf)
        {
            StartCoroutine(HideTextAfterDelay(2f));
        }*/
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.GetComponent<FixingObject>() != null)
        {
            int counter = 0;
            foreach (FixableObjectCheck check in fixableObjectChecks)
            {
                if (!check.isFixed
                    && collision.gameObject.transform.parent == null
                    && collision.gameObject.GetComponent<FixingObject>().type == check.type)
                {
                    check.isFixed = true;
                    collision.transform.position = positions[counter].position;
                    collision.transform.rotation = positions[counter].rotation;
                    FixedJoint joint = collision.gameObject.AddComponent<FixedJoint>();
                    joint.connectedBody = GetComponent<Rigidbody>();
                    collision.gameObject.tag = "MoveableObject";
                    connectedObjects.Add(collision.gameObject);
                    break;
                }
                counter++;
            }
            bool checkBool = true;
            foreach (FixableObjectCheck check in fixableObjectChecks)
            {
                checkBool = checkBool && check.isFixed;
            }
            if (checkBool && !isFixed)
            {
                GetComponent<SaveAndLoad>().Save();
                foreach (GameObject go in connectedObjects)
                {
                    go.GetComponent<SaveAndLoad>().Save();
                }
                Fix();
                //fixedText.SetActive(true);
            }
        }
    }

    protected virtual void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.GetComponent<FixingObject>() != null)
        {
            int counter = 0;
            foreach (FixableObjectCheck check in fixableObjectChecks)
            {
                if (!check.isFixed && collision.gameObject.GetComponent<FixingObject>().type == check.type && collision.gameObject.transform.parent.gameObject.layer != LayerMask.NameToLayer("Pizza"))
                {
                    connectedObjects.Add(collision.gameObject);
                    check.isFixed = true;
                    Debug.Log(gameObject.name);
                    collision.transform.position = positions[counter].position;
                    collision.transform.rotation = positions[counter].rotation;
                    FixedJoint joint = collision.gameObject.AddComponent<FixedJoint>();
                    joint.connectedBody = GetComponent<Rigidbody>();
                    collision.gameObject.tag = "MoveableObject";
                    //collision.GetComponent<Collider>().isTrigger = true;
                    break;
                }
                counter++;
            }
            bool checkBool = true;
            foreach (FixableObjectCheck check in fixableObjectChecks)
            {
                checkBool = checkBool && check.isFixed;
            }
            if (checkBool && !this.isFixed)
            {
                GetComponent<SaveAndLoad>().Save();
                foreach (GameObject go in connectedObjects)
                {
                    go.GetComponent<SaveAndLoad>().Save();
                }
                Fix();
                //fixedText.SetActive(true);
            }
        }
    }

    private IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        //fixedText.SetActive(false);
    }

    public bool getFixed()
    {
        return this.isFixed;
    }


}

