using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JEANSMODE : MonoBehaviour
{
    [SerializeField] private Material JEANS;
    private void Start()
    {
        if (PlayerPrefs.GetInt("JEANS") == 1)
        {
            SetAllMaterials(JEANS);
        }
    }

    public void SetAllMaterials(Material material)
    {
        Queue<GameObject> goQueue = new Queue<GameObject>();
        foreach (GameObject go in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            goQueue.Enqueue(go);
        }
        GameObject curr;
        while (goQueue.Count > 0)
        {
            curr = goQueue.Dequeue();
            for (int i = 0; i < curr.transform.childCount; ++i)
            {
                goQueue.Enqueue(curr.transform.GetChild(i).gameObject);
            }
            if (curr.GetComponent<Renderer>() != null)
            {
                curr.GetComponent<Renderer>().material = material;
            }
        }
    }
}
