using System.Collections;
using UnityEngine;

public abstract class Move : ScriptableObject
{
    [SerializeField] protected AudioClip[] sfx;

    public string InGameName;
    public int Damage;
    public MoveType Type;
    public string AnimationName;
    public bool ForceStandStill;
    public bool IsCancelable;
    
    public virtual void OnAnimationStart(CombatEntity context) 
    {
        if(sfx.Length == 0)
            return;
        context.GetComponent<AudioSource>().PlayOneShot(sfx[Random.Range(0,sfx.Length)]);
        
    }
    
    public virtual void OnAnimationEnd(CombatEntity context)
    {
        context.GetComponent<AudioSource>().Stop();
    }
    
    public abstract void Execute(CombatEntity context);

    public virtual void OnCancel(CombatEntity context)
    {}
}