using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefHandler : MonoBehaviour
{
    public MonoBehaviour[] handler;

    public void SetDialogueChain(DialogueChain c)
    {
        handler[0] = c;
    }
}
