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

    [SerializeField] GameObject lazer;
    private void Update()
    {
        if (allFixed && button.buttonDown)
        {
            lazer.SetActive(true);
            //try to get real lazer forward
            RaycastHit hit2;
            if (Physics.Raycast(lazer.gameObject.transform.position, lazer.gameObject.transform.forward, out hit2, LayerMask.GetMask("Pizza")))
            //if (Physics.Raycast(lazer.gameObject.transform.position, lazer.gameObject.transform.forward, out hit2))
            {
                GameObject go = null;
                if (hit2.transform.gameObject.GetComponent<Rigidbody>() != null)
                {
                    go = hit2.transform.gameObject;
                }
                currentGameObject = go;
                if (currentCoroutine == null)
                {
                    lastGameObject = go;
                    StartCoroutine(GameObjectTimer());
                }
                Debug.DrawLine(lazer.gameObject.transform.position, hit2.point, Color.green);
                //Debug.DrawLine(transform.position, hit2.point, Color.green);
            }



            //old stuff
            // RaycastHit hit;
            // if (Physics.Raycast(transform.position, transform.forward, out hit, LayerMask.GetMask("Pizza")))
            // {
                
            //     Debug.DrawLine(transform.position, hit.point, Color.green);
            //     GameObject go = null;
            //     if (hit.transform.gameObject.GetComponent<Rigidbody>() != null)
            //     {
            //         go = hit.transform.gameObject;
            //     }
            //     currentGameObject = go;
            //     if (currentCoroutine == null)
            //     {
            //         lastGameObject = go;
            //         StartCoroutine(GameObjectTimer());
            //     }
            // }
        }else{
            lazer.SetActive(false);
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
