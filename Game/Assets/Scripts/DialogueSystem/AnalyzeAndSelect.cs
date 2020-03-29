using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyzeAndSelect : MonoBehaviour
{
    public DialogueChain karma_good;
    public DialogueChain karma_bad;
    public DialogueChain karma_neu;

    public int karma_min_good;
    public int karma_min_bad;

    private void Start()
    {
        if (GameManagerScript.Instance.karma >= karma_min_good)
        {
            GetComponent<RefHandler>().SetDialogueChain(karma_good);
        }else if(GameManagerScript.Instance.karma<= karma_min_bad)
        {
            GetComponent<RefHandler>().SetDialogueChain(karma_bad);

        }
        else
        {
            GetComponent<RefHandler>().SetDialogueChain(karma_neu);

        }
    }
}
