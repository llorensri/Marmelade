using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float[] deltaSpeeds;
    public int child_reference;
    // Update is called once per frame
    void Update()
    {
        if (!CharacterController2D.block_input)
        {
            int i = 0;
            foreach (Transform c in transform)
            {
                c.transform.position += Vector3.right * Input.GetAxis("Horizontal") *  (FindObjectOfType<CharacterController2D>().speed_ * deltaSpeeds[i] *Time.deltaTime);
                ++i;
            }
        }
    }
}
