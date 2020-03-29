using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
public class StorytellerDialogue : DialogueBase
{
    public override bool Initialize(DialogueChain data)
    {

        if (data)
        {
            CharacterController2D.block_input = true;

            chain = data;
            _GUIText = GetComponent<TextMeshProUGUI>();
            _initialized = true;
            return true;
        }
        else
        {
            print("CHAIN DATA IS NULL" +
                ": ");
            return false;
        }
    }

    public override void Action()
    {

        if (chain != null)
        {
            if (Initialize(chain))
            {
                _index = 0;
                chain.data[_index].pre_execution_event.Invoke();
                StartCoroutine(chain.data[_index].Type(_GUIText));
            }
        }
        else { print("DATA IS NULL"); }
    }
    public override void Action(DialogueChain data)
    {
        if (!_initialized)
        {
            if (Initialize(data))
            {
                _index = 0;
                chain.data[_index].pre_execution_event.Invoke();
                StartCoroutine(chain.data[_index].Type(_GUIText));
            }
        }
        else
        {
            print("HEY BOY, STILL HERE, GIMME TIME!");
        }
    }


    private void FixedUpdate()
    {
        if (_initialized && !chain.data[_index].IsWriting && Input.GetButtonDown("Action"))
        {
            ++_index;

            if (_index == chain.data.Count)
            {
                _GUIText.SetText("");
                CharacterController2D.block_input = false;
                _initialized = false;
                chain.data[_index - 1].post_execution_event.Invoke();

            }
            else
            {
                StartCoroutine(chain.data[_index].Type(_GUIText));
            }

        }
    }




}