using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTrigger : MonoBehaviour
{
    [SerializeField] private int AreaNumber;
    [SerializeField] private Vector3 position;
    [SerializeField] private Quaternion rotation;

    public void EnterCheckpoint()
    {
        //PlayMusic(AreaNumber);
        FindObjectOfType<PlayerSave>().SetArea(position, rotation);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerSave>() != null)
        {
            EnterCheckpoint();
        }
    }
}
