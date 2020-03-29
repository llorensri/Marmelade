using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionDecisive : MonoBehaviour
{

    public UnityEvent onEvent;


    private void OnEnable()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            gameObject.SetActive(false);
            FindObjectOfType<StorytellerDialogue>().END();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            onEvent.Invoke();
            gameObject.SetActive(false);

        }
    }

}
