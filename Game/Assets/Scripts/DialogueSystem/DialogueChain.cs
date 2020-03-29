#define _DEBUG
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
//TODO: Quick Fixaround, try to improve it
[System.Serializable]
public class DialogueData
{
    public enum TypeType
    {
        perLetter,
        perWord
    }

    [Header("Keys")]
    public string key_;
    [Range(-1, 1)]
    public int karma_counter = 0;
    public TypeType typeStyle;
    [Range(.005f, .5f)]
    public float typeSpeed = .03f;
    private string text;
    [Space(5)]
    [Header("Events")]
    [SerializeField]
    public UnityEvent pre_execution_event;
    [SerializeField]
    public UnityEvent post_execution_event;

    private bool _isWriting;
    public bool IsWriting { get { return _isWriting; } }
    public Entry entry;

    void PrepareBubble(GameObject text, GameObject referenceWorld, Vector3 offset)
    {
        AlwaysFollowCharacter reference = Object.FindObjectOfType<AlwaysFollowCharacter>();

        if (reference)
        {
            reference.Add(referenceWorld.transform);
        }
        text.transform.parent.GetComponent<Image>().enabled = true;


        foreach(Transform c in text.transform.parent)
        {
            if (c.GetComponent<Text>())
            {
                c.GetComponent<Text>().enabled = true;
            }
        }

        if (entry.Animal == "PAN")
        {
            text.transform.parent.GetComponent<Image>().color = Object.FindObjectOfType<DialogueManager>().pj_color[0];

        }
        else if (entry.Animal == "LIN")
        {
            text.transform.parent.GetComponent<Image>().color = Object.FindObjectOfType<DialogueManager>().pj_color[1];
        }
        else if (entry.Animal == "CHA")
        {
            text.transform.parent.GetComponent<Image>().color = Object.FindObjectOfType<DialogueManager>().pj_color[2];
        }
        else if (entry.Animal == "TUC")
        {
            text.transform.parent.GetComponent<Image>().color = Object.FindObjectOfType<DialogueManager>().pj_color[3];
        }
        else if (entry.Animal == "KOA")
        {
            text.transform.parent.GetComponent<Image>().color = Object.FindObjectOfType<DialogueManager>().pj_color[4];
        }
        else if (entry.Animal == "CHI")
        {
            text.transform.parent.GetComponent<Image>().color = Object.FindObjectOfType<DialogueManager>().pj_color[5];
        }
        else
        {
            text.transform.parent.GetComponent<Image>().color = Object.FindObjectOfType<DialogueManager>().pj_color[6];

        }

        if (entry.Animal != "PRO")
        { 
            if (entry.Animal == "CHI")
            {
                GameObject.Find("KOA").GetComponent<FaceManager>().SetFace(entry.Inflection);
                GameObject.Find("KOA").transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                GameObject.Find(entry.Animal).GetComponent<FaceManager>().SetFace(entry.Inflection);
                GameObject.Find(entry.Animal).transform.GetChild(0).gameObject.SetActive(false);
            }

            Object.FindObjectOfType<PlaceUIElementAtWorld>().MoveToClickPoint(referenceWorld.transform.position + offset);
            Object.FindObjectOfType<MusicEventsManager>().PlayClip(entry.Animal, entry.Inflection);
        }

    }

    //PARSE ALL DATA FROM CSV NOT ONLY TEXT
    public bool ParseData(GameObject obj)
    {
        entry = CSVParser.GetKey(key_);
        text = entry.Text;
        if (entry.Animal == "CHI")
        {
            PrepareBubble(obj, GameObject.FindGameObjectWithTag("KOA"), new Vector3(0, 3.6f, 0));

        }
        else
        {
            PrepareBubble(obj, GameObject.FindGameObjectWithTag(entry.Animal), new Vector3(0, 3.6f, 0));
        }

        return true;
    }

    public IEnumerator Type(Text _GUIText)
    {
        Debug.Log("I ENTER HERE");
        _isWriting = true;

        ParseData(_GUIText.gameObject);
        _GUIText.text = "";
        switch (typeStyle)
        {
            case TypeType.perLetter:
                foreach (char c in text)
                {
                    _GUIText.text += c;
#if (!_DEBUG)
                    yield return new WaitForSeconds(typeSpeed);
#else
                    yield return new WaitForSeconds(0);

                    // yield return new WaitForSeconds(.025f);
#endif
                }
                break;
            case TypeType.perWord:
                string[] split = text.Split(' ');
                foreach (string str in split)
                {
                    _GUIText.text += str + " ";
#if (!_DEBUG)
                    yield return new WaitForSeconds(typeSpeed);
#else
                    yield return new WaitForSeconds(0);
                    //  yield return new WaitForSeconds(.025f);
#endif
                }
                break;
        }
        _isWriting = false;
        yield return null;
    }
}

public class DialogueChain : MonoBehaviour
{
    public List<DialogueData> data = new List<DialogueData>();
}