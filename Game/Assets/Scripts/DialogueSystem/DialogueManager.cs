using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static GameObject canvas_txt;
    public  GameObject canvas_txt_;

    public Color[] pj_color;



    private void Awake()
    {
        canvas_txt = canvas_txt_;

    }
}
