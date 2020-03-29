using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.UI;
public abstract class DialogueBase : MonoBehaviour
{
    [Header("Keyflow Here")]
    public DialogueChain chain;
    protected Text _GUIText;

    public abstract void END();
    public void ENDDAY() { GameManagerScript.Instance.AdvanceDay(); }


    public virtual bool Initialize(DialogueChain chain) { print("I should not be executing!"); return false; }

    public abstract void Action();
    public virtual void Action(DialogueChain chain) {  }

    protected int _index = 0;
    protected bool _initialized = false;
    public bool Initialized { get { return _initialized; } }


}
