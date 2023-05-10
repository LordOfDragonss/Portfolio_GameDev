using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    public bool playedAnim = false;
    public AttackState(Animator animator, Rigidbody2D rigidbody, Transform transform, Transform playerPosition, StateMachine statemachine) : base(animator, rigidbody, transform, playerPosition, statemachine)
    {
    }

    public override void OnEnter()
    {
        Vector3 targetpos = stateMachine.getPlayerPos();
        targetpos.y = trans.position.y; ;
        //look at player
        trans.GetChild(0).LookAt(targetpos);

        base.OnEnter();
        stateMachine.canAttack = false;
        // Add additional code that should be executed when entering the MovingState
        
    }

    public override void Update()
    {
        //base.Update();
        // Add code that updates the MovingState
        // Debug.Log("normalized time: " + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        if (anim.GetCurrentAnimatorClipInfo(0).Length > 0)
        {
            if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Attack") )
            {
                playedAnim = true;
            }
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && playedAnim)
        {
            SwitchState();
            return;
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        playedAnim = false;
        stateMachine.StartCoroutine(stateMachine.enableAttack());
        moveSet.CurrentMoveAnimationEnded();
        moveSet.currentMove = null;
        // Add additional code that should be executed when exiting the MovingState
    }

    public override void SwitchState()
    {
        base.SwitchState();
        //Canceling Switch if transition still busy

    }


}
