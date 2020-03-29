using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CinematicGameIntro : MonoBehaviour
{
    public GameObject FrontFade;
    public GameObject Presentation;
    public GameObject Flow;
    public UnityEvent ev;
    public void Start()
    {
        CharacterController2D.block_input = true;
    }

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
        CharacterController2D.block_input = false;

    }

    public void StartGame()
    {
        ev.Invoke();
    }
}
