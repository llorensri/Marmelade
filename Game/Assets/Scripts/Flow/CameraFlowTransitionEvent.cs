using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlowTransitionEvent : MonoBehaviour
{
    public GameObject flow; 
    public void startInGameCameraFlow()
    {
        flow.SetActive(true);
    }
}
