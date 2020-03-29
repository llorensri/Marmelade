using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GetColor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       Color t=  transform.parent.GetComponent<Image>().color;
        float h, s, v;
        Color.RGBToHSV(t, out h, out s, out v);
        v /= 4;
        t = Color.HSVToRGB(h, s, v);
        t.a = 1.0f;
        GetComponent<Text>().color = t;
    }
}
