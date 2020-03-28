using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceManager : MonoBehaviour
{
    public float offset = 1.0f;
    public void SetFace(string c)
    {
        int i = 0;
        if (c=="NEU")
        {

        }else if (c == "HAP")
        {
            i = 1;
        }
        else if (c == "SAD")
        {
            i = 2;

        }
        else if (c == "ANG")
        {
            i = 3;
        }
        else if (c == "FEA")
        {
            i = 4;
        }
        else if (c == "SUR")
        {
            i = 5;
        }
        else if (c == "DIS")
        {
            i = 6;
        }
        else if (c == "CON")
        {
            i = 7;
        }
        transform.Find("Face").transform.position = new Vector3(-(i* offset),0,0);
    }

}
