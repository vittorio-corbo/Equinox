using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAntenna : Reportee
{
    [SerializeField] ButtonScript button;
    GameObject currentGameObject;
    GameObject lastGameObject;
    float timer;
    Coroutine currentCoroutine;
    private void Update()
    {
        if (allFixed && button.buttonDown)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, LayerMask.GetMask("Pizza")))
            {
                GameObject go = null;
                if (hit.transform.gameObject.GetComponent<Rigidbody>() != null)
                {
                    go = hit.transform.gameObject;
                }
                currentGameObject = go;
                if (currentCoroutine == null) 
                {
                    lastGameObject = go;
                    StartCoroutine(GameObjectTimer());
                }
            }
        }
    }
    private IEnumerator GameObjectTimer()
    {
        while (timer < 5f)
        {
            if (currentGameObject == null)
            {
                yield break;
            }
            if (!currentGameObject.Equals(lastGameObject))
            {
                timer = 0f;
            }
            lastGameObject = currentGameObject;
            yield return new WaitForSeconds(.1f);
            timer += .1f;
        }
        currentGameObject.GetComponent<Rigidbody>().isKinematic = false;
        currentGameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(0, 100), Random.Range(0, 100), Random.Range(0, 100)), ForceMode.Impulse);
    }
}
