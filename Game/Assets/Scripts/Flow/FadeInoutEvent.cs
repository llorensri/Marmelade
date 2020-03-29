using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FadeInoutEvent : MonoBehaviour
{
    public int eventToTrigger;
    public UnityEvent[] events;
    public UnityEvent[] post_events;

    public void TriggerEvent()
    {
        events[eventToTrigger].Invoke();
    }

    public void Kill()
    {
        post_events[eventToTrigger].Invoke();
        gameObject.SetActive(false);
    }
}
