
using System;
using UnityEngine;
using UnityEngine.Events;

using TMPro;

public class DialogueQTE : DialogueBase
{
    [Header("Keyflow Here")]
    private DialogueData First;
    private DialogueData Second;
    private DialogueData Third;
    private TextMeshProUGUI _GUITextFirst, _GUITextSecond, _GUITextThird;
    GameObject DialogueRef;

    private bool _actionPressed = false;
#if (_DEBUG)
    private void Start()
    {
        Initialize();
        Action();
    }
#endif



    protected override bool Initialize(DialogueChain data)
    {
        if (data.data.Count < 1)
        {
            return false;
        }
        First = data.data[0];

        if (data.data.Count > 1)
        {
            Second = data.data[1];
        }
        if (data.data.Count > 2)
        {
            Third = data.data[2];
        }

        DialogueRef = GameObject.Find("SelectionTexts");
        DialogueRef.SetActive(true);
        try
        {
            _GUITextFirst = (TextMeshProUGUI)DialogueRef.GetComponent<RefHandler>().handler[0];
            _GUITextSecond = (TextMeshProUGUI)DialogueRef.GetComponent<RefHandler>().handler[1];
            _GUITextThird = (TextMeshProUGUI)DialogueRef.GetComponent<RefHandler>().handler[2];
            _GUITextFirst.text = "";
            _GUITextSecond.text = "";
            _GUITextThird.text = "";
            _actionPressed = false;

            _initialized = true;
            return true;
        }
        catch (Exception e)
        {
            print("DIALOGUE SELECTION COMPONENT NOT FOUND: " + e.ToString());
            return false;
        }
    }

    public override void Action()
    {
        if (chain != null)
        {
            Action(chain);
        }
        else
        {
            print("DIALOGUE QTE ERROR: NO DIALOGUE SELECTED");
        }
    }
    public override void Action(DialogueChain chain)
    {
        if (Initialize(chain))
        {
            _index = 0;
            chain.data[_index].pre_execution_event.Invoke();
            StartCoroutine(First.Type(_GUITextFirst));
            if (Second != null)
                StartCoroutine(Second.Type(_GUITextSecond));
            if (Third != null)
                StartCoroutine(Third.Type(_GUITextThird));
        }
    }
    private bool anyWriting()
    {
        return (First.IsWriting | (Second != null && Second.IsWriting) | (Third != null && Third.IsWriting));
    }

    private void Update()
    {
        if (_initialized && !anyWriting())
        {
            //TODO: CHANGE THIS FOR A BUTTON
            if (Input.GetKeyDown(KeyCode.Z))
            {
                First.post_execution_event.Invoke();
                _actionPressed = true;
            }
            else if ((Second != null) && Input.GetKeyDown(KeyCode.X))
            {
                Second.post_execution_event.Invoke();
                _actionPressed = true;

            }
            else if ((Third != null) && Input.GetKeyDown(KeyCode.C))
            {
                Third.post_execution_event.Invoke();
                _actionPressed = true;

            }

            if (_actionPressed)
            {
                //TODO: HIDE/SHOW TEXT IN A BETTER WAY
                DialogueRef.SetActive(false);
                //TODO: DOTween to Zoom in when a Dialogue starts ends
                //TODO: Block Unblock Movement when commenting
                _initialized = false;
            }
        }
    }
}
