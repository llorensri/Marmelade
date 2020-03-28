using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysFollowCharacter : MonoBehaviour
{
    public Cinemachine.CinemachineTargetGroup actuallyFollowing;
    // Start is called before the first frame update
    void Start()
    {
        actuallyFollowing = GetComponent<Cinemachine.CinemachineTargetGroup>();
        actuallyFollowing.AddMember(GameObject.Find("Character").transform, 2, 1);
    }

   public void Add(Transform obj)
    {
        if(actuallyFollowing.FindMember(obj)==-1)
            actuallyFollowing.AddMember(obj, .75f, 4);
    }

    public void Remove() {
        Array.Clear(actuallyFollowing.m_Targets, 0, actuallyFollowing.m_Targets.Length);
        actuallyFollowing.AddMember(GameObject.Find("Character").transform, 2, 1);
    }
}
