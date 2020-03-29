using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStoryTeller_Mod : MonoBehaviour
{

    public DialogueChain First;
    public DialogueChain Second;
    public DialogueChain Third;
    private bool update = false;
    // Start is called before the first frame update
    void Start()
    {
        //FindObjectOfType<StoryTeller_Mod>().setData(First, Second, Third);
    }

    // Update is called once per frame
    void Update()
    {
        if (!update)
        {
            FindObjectOfType<StoryTeller_Mod>().setData(First, Second, Third);
            update = true;
        }
        
    }


}
