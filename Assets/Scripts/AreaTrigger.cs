using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTrigger : MonoBehaviour
{
    [SerializeField] private int AreaNumber;
    [SerializeField] private AudioClip Music;
    [SerializeField] private Vector3 position;
    [SerializeField] private Quaternion rotation;

    public MusicReportee musicReportee;
    public Music musicGenerator;

    public bool inArea;

    public void EnterCheckpoint()
    {
        inArea = true;
        StartCoroutine(StartMusicInNewArea());
        FindObjectOfType<PlayerSave>().SetArea(position, rotation);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerSave>() != null)
        {
            EnterCheckpoint();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerSave>() != null)
        {
            inArea = false;
            musicGenerator.StopMusic();
        }
    }

    public IEnumerator StartMusicInNewArea()
    {
        yield return new WaitUntil(() => musicGenerator.isPlaying == false);
        musicReportee.GatherReport();
    }
}
