using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipperAttackState : AttackState
{
    public SlipperAttackState(Animator animator, Rigidbody2D rigidbody, Transform transform, Transform playerPosition, StateMachine statemachine) : base(animator, rigidbody, transform, playerPosition, statemachine)
    {
    }

    public override void OnEnter()
    {
        // Add additional code that should be executed when entering the MovingState
        anim.SetTrigger("SlipperAttack");
        moveSet.DoSpecial1Attack();
        base.OnEnter();
    }

    public override void Update()
    {
        //base.Update();
        if (anim.GetCurrentAnimatorClipInfo(0).Length > 0)
        {
            if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Slipper"))
            {
                playedAnim = true;
            }
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && playedAnim)
        {
            Debug.Log("switch");
            SwitchState();
            return;
        }

    }

    public override void OnExit()
    {
        stateMachine.StartCoroutine(stateMachine.enableSpecialAttack());

        base.OnExit();
        // Add additional code that should be executed when exiting the MovingState
    }

    public override void SwitchState()
    {
        
        if (!canTransition)
        {
            //Debug.Log("Cant transition");
            return;
        }

        float distance = trans.position.x - playerPos.position.x;
        if (Mathf.Abs(distance) > attackRange)
        {
            //moving back to player if out of attack range
            Debug.Log("moving");
            stateMachine.nextState = new MovingState(anim, rb, trans, playerPos, stateMachine);
        }
        else
        {
            Debug.Log("Idle state");
            //returning to idle
            stateMachine.nextState = new IdleState(anim, rb, trans, playerPos, stateMachine);
        }
    }




}
