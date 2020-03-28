using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPC : MonoBehaviour
{

    public UnityEvent onAction;


    void Action()
    {
        FindObjectOfType<StorytellerDialogue>().chain = (DialogueChain)GetComponent<RefHandler>().handler[0];
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("Character")) { 
        transform.GetChild(0).gameObject.SetActive(true);
        Action();
        FindObjectOfType<CharacterController2D>().eventToTrigger = onAction;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            transform.GetChild(0).gameObject.SetActive(false);
            FindObjectOfType<CharacterController2D>().eventToTrigger.RemoveAllListeners();
        }
    }

}
