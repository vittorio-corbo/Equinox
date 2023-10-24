using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Laser;

public class CrossHair : MonoBehaviour
{
    [SerializeField] private Sprite stopperCrossHair;
    [SerializeField] private Sprite grabbableCrossHair;
    [SerializeField] private Sprite defaultCrossHair;
    private Image curImage;

    public enum CrossHairStates {Stopper, Glass, Grabbable, DefaultObject, NoObject}
    private CrossHairStates curState;
    void Start()
    {
        curImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (curState)
        {
            case CrossHairStates.Stopper:
                curImage.sprite = stopperCrossHair;
                curImage.color = Color.black;
                break;
            case CrossHairStates.Glass:
                curImage.sprite = defaultCrossHair;
                curImage.color = Color.white;
                break;
            case CrossHairStates.Grabbable:
                curImage.sprite = grabbableCrossHair;
                curImage.color = Color.blue;
                break;
            case CrossHairStates.DefaultObject:
                curImage.sprite = defaultCrossHair;
                curImage.color = Color.red;
                break;
            case CrossHairStates.NoObject:
                curImage.color = new Color(0, 0, 0, 0);
                break;
        }
    }

    private void updateCrossHairState(CrossHairStates newState)
    {
        curState = newState;
    }

    private void OnEnable()
    {
        Laser.switchCrossHair += updateCrossHairState;
    }

    private void OnDisable()
    {
        Laser.switchCrossHair -= updateCrossHairState;
    }
}
