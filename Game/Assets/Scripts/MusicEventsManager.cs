using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicEventsManager : MonoBehaviour
{
    [Header("HAP-SAD-ANG-FEA-SUR-DIS-CON")]
    public AudioClip[] KOA_audioSources;
    public AudioClip[] PAN_audioSources;
    public AudioClip[] TUC_audioSources;
    public AudioClip[] CHA_audioSources;
    public AudioClip[] LIN_audioSources;
    public AudioClip[] CHI_audioSources;

    public void PlayClip(string animal, string c)
    {
        int i = 0;
       if (c == "HAP")
        {
            i = 0;
        }
        else if (c == "SAD")
        {
            i = 1;

        }
        else if (c == "ANG")
        {
            i = 2;
        }
        else if (c == "FEA")
        {
            i = 3;
        }
        else if (c == "SUR")
        {
            i = 4;
        }
        else if (c == "DIS")
        {
            i = 5;
        }
        else if (c == "CON")
        {
            i = 6;
        }
        print("SelectedSound is " + i);
        AudioClip refer = null;
        if (animal == "KOA")
        {
            refer = KOA_audioSources[i];
        } else if (animal == "PAN")
        {
            refer = PAN_audioSources[i];

        }
        else if (animal == "TUC")
        {
            refer = TUC_audioSources[i];

        }
        else if (animal == "CHA")
        {
            refer = CHA_audioSources[i];

        }
        else if (animal == "LIN")
        {
            refer = LIN_audioSources[i];

        }else if (animal == "CHI")
        {
            refer = CHI_audioSources[i];

        }
        if (refer != null)
        {
            print("I PLAY SOUND");
            GetComponent<AudioSource>().clip = refer;
            GetComponent<AudioSource>().Play();
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
}
