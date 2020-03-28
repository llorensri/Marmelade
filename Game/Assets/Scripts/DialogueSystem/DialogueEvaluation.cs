using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
public class DialogueEvaluation : DialogueBase
{
    public int karma_good, karma_bad;

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
    private DialogueChain Evaluate()
    {
        RefHandler handler = GetComponent<RefHandler>();
        if (GameManagerScript.Instance.karma >= karma_good)
        {
            return (DialogueChain)handler.handler[0];
        }else if (GameManagerScript.Instance.karma <= karma_bad)
        {
            return (DialogueChain)handler.handler[1];
        }
        else
        {
            return (DialogueChain)handler.handler[2];
        }
    }
    public override void Action()
    {
        if (!_initialized)
        {
            
            if (chain != null)
            {
                if (Initialize(Evaluate()))
                {
                    _index = 0;
                    chain.data[_index].pre_execution_event.Invoke();
                    StartCoroutine(chain.data[_index].Type(_GUIText));
                }
            }
            else { print("DATA IS NULL"); }
        }
    }

    private void Update()
    {
        if (_initialized && !chain.data[_index].IsWriting && Input.GetButtonDown("Action"))
        {
            ++_index;

            if (_index == chain.data.Count)
            {
                _initialized = false;
                _GUIText.SetText("");
                CharacterController2D.block_input = false;
                chain.data[_index - 1].post_execution_event.Invoke();

            }
            else
            {
                chain.data[_index - 1].post_execution_event.Invoke();
                StartCoroutine(chain.data[_index].Type(_GUIText));
            }

        }
    }




}
