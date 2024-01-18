using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldPrompt : MonoBehaviour
{
    public GameObject holdText;
    public GameObject releaseText;
    public PlayerGrapple player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerGrapple>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, 5f, (~LayerMask.GetMask("NotHoldable") & ~LayerMask.GetMask("GrappleHead"))) && player.holding == false && !PauseScript.isPaused)
        {
            holdText.SetActive(true);
            releaseText.SetActive(false);
        }
        else
        {
            if (player.holding == true && !PauseScript.isPaused){
                holdText.SetActive(false);
                releaseText.SetActive(true);
            }else{
                holdText.SetActive(false);
                releaseText.SetActive(false);
            }
            

            //old code
            //holdText.SetActive(false);
        }
    }
}
