using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        AlwaysFollowCharacter reference= Object.FindObjectOfType<AlwaysFollowCharacter>();
        reference.Add(referenceWorld.transform);
        
        //TODO: THIS

    }

    //PARSE ALL DATA FROM CSV NOT ONLY TEXT
    public bool ParseData(GameObject obj)
    {
        entry = CSVParser.GetKey(key_);
        text = entry.Text;

        PrepareBubble(obj, GameObject.FindGameObjectWithTag(entry.Animal), new Vector3(0, 30, 0));
        
        return true;
    }

    public IEnumerator Type(TextMeshProUGUI _GUIText)
    {

        ParseData(_GUIText.gameObject);
        _isWriting = true;
        _GUIText.SetText("");
        switch (typeStyle)
        {
            case TypeType.perLetter:
                foreach (char c in text)
                {
                    _GUIText.text += c;
                    yield return new WaitForSeconds(typeSpeed);
                }
                break;
            case TypeType.perWord:
                string[] split = text.Split(' ');
                foreach (string str in split)
                {
                    _GUIText.text += str + " ";
                    yield return new WaitForSeconds(typeSpeed);
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


