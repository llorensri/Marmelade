﻿
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using TMPro;

public class DialogueQTE : DialogueBase
{
    [Header("Keyflow Here")]
    private DialogueData First;
    private DialogueData Second;
    private DialogueData Third;
    private Text _GUITextFirst, _GUITextSecond, _GUITextThird;
    GameObject DialogueRef;
    public float decisionSeconds = 1.5f;

    private int _actionPressed = 0;

    public override bool Initialize(DialogueChain data)
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
            _GUITextFirst = (Text)DialogueRef.GetComponent<RefHandler>().handler[0];
            _GUITextSecond = (Text)DialogueRef.GetComponent<RefHandler>().handler[1];
            _GUITextThird = (Text)DialogueRef.GetComponent<RefHandler>().handler[2];
            _GUITextFirst.text = "";
            _GUITextSecond.text = "";
            _GUITextThird.text = "";
            _actionPressed = 0;
            CharacterController2D.block_input = true;

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
    public override void END()
    {
        foreach (Transform t in transform)
        {
            t.GetComponent<Image>().enabled = false;
        }
        CharacterController2D.block_input = false;
    }
    private void Update()
    {
        if (_initialized && !anyWriting())
        {
            //TODO: CHANGE THIS FOR A BUTTON
            if (Input.GetKeyDown(KeyCode.Z))
            {
                _actionPressed = 1;
                _GUITextSecond.text="";
                _GUITextSecond.transform.parent.GetComponent<Image>().enabled = false;
                foreach (Transform a in _GUITextSecond.transform.parent)
                {
                    if (a.GetComponent<Text>())
                    {
                        a.GetComponent<Text>().enabled = false;
                    }
                }
                _GUITextSecond.transform.parent.GetComponent<Image>().enabled = false;

                _GUITextThird.text = "";
                foreach (Transform a in _GUITextThird.transform.parent)
                {
                    if (a.GetComponent<Text>())
                    {
                        a.GetComponent<Text>().enabled = false;
                    }
                }
                _GUITextThird.transform.parent.GetComponent<Image>().enabled = false;

                GameObject.Find("Character").GetComponent<FaceManager>().SetFace(First.entry.Inflection);
            }
            else if ((Second != null) && Input.GetKeyDown(KeyCode.X))
            {

                _actionPressed = 2;
                _GUITextThird.text = "";
                foreach (Transform a in _GUITextThird.transform.parent)
                {
                    if (a.GetComponent<Text>())
                    {
                        a.GetComponent<Text>().enabled = false;
                    }
                }
                _GUITextThird.transform.parent.GetComponent<Image>().enabled = false;

                _GUITextFirst.text = "";
                foreach (Transform a in _GUITextFirst.transform.parent)
                {
                    if (a.GetComponent<Text>())
                    {
                        a.GetComponent<Text>().enabled = false;
                    }
                }
                _GUITextFirst.transform.parent.GetComponent<Image>().enabled = false;

                GameObject.Find("Character").GetComponent<FaceManager>().SetFace(Second.entry.Inflection);
            }
            else if ((Third != null) && Input.GetKeyDown(KeyCode.C))
            {

                _actionPressed = 3;
                _GUITextSecond.text = "";
                foreach (Transform a in _GUITextSecond.transform.parent)
                {
                    if (a.GetComponent<Text>())
                    {
                        a.GetComponent<Text>().enabled = false;
                    }
                }
                _GUITextSecond.transform.parent.GetComponent<Image>().enabled = false;
                _GUITextFirst.text = "";
                foreach (Transform a in _GUITextFirst.transform.parent)
                {
                    if (a.GetComponent<Text>())
                    {
                        a.GetComponent<Text>().enabled = false;
                    }
                }
                _GUITextFirst.transform.parent.GetComponent<Image>().enabled = false;

                GameObject.Find("Character").GetComponent<FaceManager>().SetFace(Third.entry.Inflection);
            }

            if (_actionPressed != 0)
            {
                StartCoroutine(waitResponse());
            }
        }
    }

    public IEnumerator waitResponse()
    {
        yield return new WaitForSeconds(decisionSeconds);
        _initialized = false;
        switch (_actionPressed)
        {
            case 1:
                GameManagerScript.Instance.karma += First.karma_counter;

                First.post_execution_event.Invoke();

                break;
            case 2:
                GameManagerScript.Instance.karma += Second.karma_counter;

                Second.post_execution_event.Invoke();

                break;
            case 3:
                GameManagerScript.Instance.karma += Third.karma_counter;

                Third.post_execution_event.Invoke();

                break;
        }
        _actionPressed = 0;

        First = null; Second = null; Third = null;

        //TODO: HIDE/SHOW TEXT IN A BETTER WAY
        foreach (Transform t in DialogueRef.transform)
        {
            t.GetComponentInChildren<Text>().text = "";
            t.GetComponent<Image>().enabled = false;

            foreach(Transform a in t)
            {
                if (a.GetComponent<Text>())
                {
                    a.GetComponent<Text>().enabled = false;
                }
            }
        }
        //TODO: DOTween to Zoom in when a Dialogue starts ends
        //TODO: Block Unblock Movement when commenting

    }
}
