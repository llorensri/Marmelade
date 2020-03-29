using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
public class StorytellerDialogue : DialogueBase
{
    public void Awake()
    {
        GetComponent<RectTransform>().localPosition = new Vector3(0, 38, 0);
    }
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

    public override void END()
    {

        transform.parent.GetComponent<Image>().enabled = false;
        CharacterController2D.block_input = false;
    }

    public override void Action()
    {

        if (chain != null)
        {
            Action(chain);
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


    private void Update()
    {
        if (Input.GetButtonDown("Action"))
        {

            if (_initialized && !chain.data[_index].IsWriting)
            {
                ++_index;

                if (_index == chain.data.Count)
                {
                    print("adasdas");
                    _GUIText.SetText("");
                    transform.parent.GetComponent<Image>().enabled = false;
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




}