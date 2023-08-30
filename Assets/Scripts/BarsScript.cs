using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarsScript : MonoBehaviour
{
    [SerializeField] private GameObject cinematicBarsContainerGO;
    [SerializeField] private Animator cinematicBarsAnimator;

    public void ShowBars()
    {
        cinematicBarsContainerGO.SetActive(true);
    }
}
