using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSet : MonoBehaviour
{
    [SerializeReference] public Move lightAttack;
    [SerializeReference] public Move heavyAttack;
    [SerializeReference] public Move special1;
    [SerializeReference] public Move special2;

    [SerializeReference] internal List<Move> learnedMoves = new();
    internal Move currentMove;

    private CombatEntity context;
    private Animator animator;

    public bool IsExecutingAttack
    {
        get { return currentMove != null; }
    }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        context = GetComponent<CombatEntity>();
    }

    public void DoLightAttack()
    {
        DoMove(lightAttack);
    }

    public void DoHeavyAttack()
    {
        DoMove(heavyAttack);
    }

    public void DoSpecial1Attack()
    {
        DoMove(special1);
    }

    public void DoSpecial2Attack()
    {
        DoMove(special2);
    }

    public void OnAttackAnimationEvent()
    {
        currentMove.Execute(context);
    }

    public void CancelMove()
    {
        StopAllCoroutines();
        currentMove.OnCancel(context);
        currentMove = null;
    }

    public bool IsBelowHealth(int health)
    {
        return context.health.CurrentHealth <= health;
    }

    private void DoMove(Move move)
    {
        if(!CompareTag("Player"))
        {
            currentMove = move;
            currentMove.OnAnimationStart(context);
            return;
        }
        if (IsExecutingAttack || Time.timeScale == 0)
            return;

        StartCoroutine(PlayAttackAnimation(move));
    }

    public void CurrentMoveAnimationEnded()
    {
        if (currentMove != null)
        {
            currentMove.OnAnimationEnd(context);
        }
    }
    
    public void HandleEnemyMove()
    {
        Debug.Log("handle enemy move");
        currentMove.Execute(context);
    }

    private IEnumerator PlayAttackAnimation(Move move)
    {
        currentMove = move;

        animator.Play(move.AnimationName);
        if (currentMove.ForceStandStill)
        {
            var lowerName = move.AnimationName + " Lower";
            if (animator.HasState(0, Animator.StringToHash(lowerName)))
            {
                animator.Play(lowerName);
            }
        }
        
        currentMove.OnAnimationStart(context);
        
        while (!animator.GetCurrentAnimatorStateInfo(1).IsName(move.AnimationName))
        {
            yield return new WaitForSeconds(0.1f);
        }
        var state = animator.GetCurrentAnimatorStateInfo(1);
        var clips = animator.GetCurrentAnimatorClipInfo(1);
        
        yield return new WaitForSeconds(clips[0].clip.length / state.speed);
        
        currentMove.OnAnimationEnd(context);
        currentMove = null;
    }
}