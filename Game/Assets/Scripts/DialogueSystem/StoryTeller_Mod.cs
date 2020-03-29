using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using TMPro;
public class StoryTeller_Mod : DialogueBase
{

    public DialogueChain First;
    public DialogueChain Second;
    public DialogueChain Third;

    public int karma_good, karma_bad;

    public override void END()
    {

        transform.parent.GetComponent<Image>().enabled = false;
        CharacterController2D.block_input = false;
    }

    public void SefFirst(DialogueChain a)
    {
        First = a;
    }
    public void SetSecond(DialogueChain b)
    {
        Second = b;
    }
    public void SetThird(DialogueChain c)
    {
        Third = c;
    }
    public void SetKarmaGood(int tst)
    {
        karma_good =tst;

    }
    public void SetKarmaBad(int tst)
    {
        karma_bad = tst;
    }

    public override bool Initialize(DialogueChain data)
    {

        if (data)
        {
            CharacterController2D.block_input = true;

            chain = data;
            _GUIText = GetComponent<Text>();
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
        DialogueChain data;
        if (GameManagerScript.Instance.karma >= karma_good)
        {
            data = First;
        }
        else if (GameManagerScript.Instance.karma <= karma_bad)
        {
            data = Third;
        }
        else
        {
            data = Second;
        }


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
        if (_initialized && !chain.data[_index].IsWriting && Input.GetButtonDown("Action"))
        {
            ++_index;

            if (_index == chain.data.Count)
            {
                _GUIText.text = "";
                _initialized = false;
                transform.parent.GetComponent<Image>().enabled = false;

                chain.data[_index - 1].post_execution_event.Invoke();

            }
            else
            {
                StartCoroutine(chain.data[_index].Type(_GUIText));
            }

        }
    }

    public void setData(DialogueChain data, DialogueChain data1, DialogueChain data2)
    {
        First = data;
        Second = data1;
        Third = data2;
    }

}