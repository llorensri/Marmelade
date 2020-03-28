using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicGameIntro : MonoBehaviour
{
    public GameObject FrontFade;
    public GameObject Presentation;
    public GameObject Flow;



    public void FrontFadeOut()
    {
        FrontFade.GetComponent<Animator>().enabled=(true);
    }

    public void FadeInOutPresentation()
    {
        Presentation.SetActive(true);
    }

    public void GiveFlowToCamera()
    {
        Flow.GetComponent<Animator>().enabled = (true);
    }
}
