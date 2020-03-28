using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.UI;
public abstract class DialogueBase : MonoBehaviour
{
    [Header("Keyflow Here")]
    public DialogueChain chain;
    protected TextMeshProUGUI _GUIText;



    public virtual bool Initialize(DialogueChain chain) { print("I should not be executing!"); return false; }

    public abstract void Action();
    public virtual void Action(DialogueChain chain) { print("I should not be executing!"); }

    protected int _index = 0;
    protected bool _initialized = false;



}
