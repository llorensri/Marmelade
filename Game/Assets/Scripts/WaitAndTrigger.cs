using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaitAndTrigger : MonoBehaviour
{
    public float seconds;
    public UnityEvent ToTrigger;
    private void OnEnable()
    {
        StartCoroutine(waitAndTrigger());
    }

    private IEnumerator waitAndTrigger() {
        yield return new WaitForSeconds(seconds);
        ToTrigger.Invoke();
        enabled = false;
    }
}
