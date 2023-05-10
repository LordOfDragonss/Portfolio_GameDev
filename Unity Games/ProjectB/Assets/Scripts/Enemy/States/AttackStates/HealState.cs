using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class HealState : AttackState
{
    private bool reachedSafeDistance;
    private float targetX;

    public HealState(Animator animator, Rigidbody2D rigidbody, Transform transform, Transform playerPosition,
        StateMachine statemachine) : base(animator, rigidbody, transform, playerPosition, statemachine)
    {
    }

    public override void OnEnter()
    {
        // Add additional code that should be executed when entering the MovingState
        anim.SetTrigger("Moving");
        var delta = trans.position.x - playerPos.position.x;
        if (delta > 0)
        {
            targetX = trans.position.x + safeHealDistance - Mathf.Abs(delta);
        }
        else
        {
            targetX = trans.position.x - safeHealDistance + Mathf.Abs(delta);
        }

        targetX = Mathf.Clamp(targetX, stateMachine.boundsLeft.transform.position.x,
            stateMachine.boundsRight.transform.position.x);
        
        Debug.Log($"from {trans.position.x} to {targetX}");

        //moveSet.DoSpecial1Attack();
        base.OnEnter();
    }

    public override void Update()
    {
        //base.Update();
        if (reachedSafeDistance)
        {
            if (anim.GetCurrentAnimatorClipInfo(1).Length > 0)
            {
                if (anim.GetCurrentAnimatorClipInfo(1)[0].clip.name.Contains("Heal"))
                {
                    Debug.Log("Played anim");
                    playedAnim = true;
                }
            }

            if (anim.GetCurrentAnimatorStateInfo(1).normalizedTime > 1 && playedAnim)
            {
                Debug.Log("switch from heal");
                SwitchState();
            }
        }
        else
        {
            float rotation = trans.position.x > targetX ? -90 : 90;

            var charTrans = trans.GetChild(0);
            trans.GetChild(0).eulerAngles = new Vector3(charTrans.eulerAngles.x, rotation, charTrans.eulerAngles.z);
            trans.position += trans.GetChild(0).forward * 2.0f * Time.deltaTime;

            if (Mathf.Abs(targetX - trans.position.x) < 0.5f)
            {
                Debug.Log("reached safe distance");
                reachedSafeDistance = true;
                anim.SetTrigger("Heal");
                moveSet.DoSpecial1Attack();
            }
        }
    }

    public override void OnExit()
    {
        stateMachine.StartCoroutine(stateMachine.enableSpecialAttack(15f));
        anim.SetTrigger("No Override");

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