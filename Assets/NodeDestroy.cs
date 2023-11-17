using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeDestroy : MonoBehaviour
{
    private Coroutine destructionCoroutine;
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<RopeNode>() != null || other.gameObject.GetComponent<NodeDestroy>() != null)
        {
            return;
        }
        destructionCoroutine = StartCoroutine(DestructionWait());
    }

    private IEnumerator DestructionWait()
    {
        yield return new WaitForSeconds(.2f);
        //transform.parent.GetComponent<RopeNode>().DestroyNode();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<RopeNode>() != null || other.gameObject.GetComponent<NodeDestroy>() != null)
        {
            return;
        }
        if (destructionCoroutine != null)
        {
            StopCoroutine(destructionCoroutine);
        }
    }
}
