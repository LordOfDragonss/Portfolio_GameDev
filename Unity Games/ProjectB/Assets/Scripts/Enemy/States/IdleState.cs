using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The IdleState class implements the behavior for the idle state of the enemy
public class IdleState : BaseState
{
    // Constructor to initialize the required components
    public IdleState(Animator animator, Rigidbody2D rigidbody, Transform transform, Transform playerPosition, StateMachine statemachine) : base(animator, rigidbody, transform, playerPosition, statemachine)
    {
    }

    // Overridden method that is called when entering the idle state
    public override void OnEnter()
    {
        // Call the base OnEnter method
        base.OnEnter();

        // Trigger the idling animation
        anim.SetTrigger("Idling");
    }

    // Overridden method that updates the state
    public override void Update()
    {
        // Check if the player has moved
        if (stateMachine.player.transform.position != stateMachine.playerSpawn)
        {
            // If the player has moved, switch to the next state
            SwitchState();
            return;
        }
    }

    // Overridden method that is called when exiting the idle state
    public override void OnExit()
    {
        // Call the base OnExit method
        base.OnExit();

        // Reset the idling animation trigger
        anim.ResetTrigger("Idling");
    }

    // Overridden method to switch to the next state based on certain conditions
    public override void SwitchState()
    {
        // Call the base SwitchState method
        base.SwitchState();

        // Check if the enemy can transition to the next state
        if (canTransition)
        {
            // Calculate the distance between the enemy and the player
            float distance = trans.position.x - playerPos.position.x;

            //Check specialmove name
            /*if(moveSet.special1.name == "Slipper Attack")
            {
                //Check if inrange to perform slipper attack
                if(distance > minSlipperRange && distance < maxSlipperRange && stateMachine.canAttack && stateMachine.canSpecial)
                {
                    float randomAttack = Random.Range(0, 100);
                    Debug.Log("Try slipper: idle");
                    if (randomAttack > 90)
                    {
                        stateMachine.nextState = new SlipperAttackState(anim, rb, trans, playerPos, stateMachine);
                        //SwitchState();
                        return;
                    }
                }
            }*/
            
            if (moveSet.special1.name.Equals("Slipper Attack"))
            {
                if (Mathf.Abs(distance) > minSlipperRange && Mathf.Abs(distance) < maxSlipperRange &&
                    stateMachine.canAttack && stateMachine.canSpecial)
                {
                    float randomAttack = Random.Range(0, 100);
                    if (randomAttack > 95)
                    {
                        stateMachine.canAttack = false;
                        stateMachine.canSpecial = false;
                        stateMachine.nextState = new SlipperAttackState(anim, rb, trans, playerPos, stateMachine);
                        //SwitchState();
                        return;
                    }
                }
            }
            else if (moveSet.special1.name.Equals("Heal Move"))
            {
                if (moveSet.IsBelowHealth(healMinHealth))
                {
                    stateMachine.canAttack = false;
                    stateMachine.canSpecial = false;
                    stateMachine.nextState = new HealState(anim, rb, trans, playerPos, stateMachine);
                    //SwitchState();
                    return;
                }
            }

            // Check if the distance is greater than the attack range
            if (Mathf.Abs(distance) > attackRange)
            {
                // If the distance is greater than the attack range, switch to the moving state
                stateMachine.nextState = new MovingState(anim, rb, trans, playerPos, stateMachine);
            }
            else
            {
                // Check if the player has moved and the enemy can attack
                if (stateMachine.playerMoved && stateMachine.canAttack)
                {
                    // Choose a random attack
                    float randomAttack = Random.Range(0, 100);

                    // Check if the random number is less than 40
                    if (randomAttack < 40)
                    {
                        // If the random number is less than 40, switch to the light attack state
                        stateMachine.nextState = new LightAttackState(anim, rb, trans, playerPos, stateMachine);
                    }
                    // Check if the random number is less than 85
                    else if (randomAttack < 85)
                    {
                        // If the random number is less than 85, switch to the heavy attack state
                        stateMachine.nextState = new HeavyAttackState(anim, rb, trans, playerPos, stateMachine);
                    }
                    else
                    {
                        // If the random number is greater than or equal to 85, switch to the special move state
                        stateMachine.nextState = new LightAttackState(anim, rb, trans, playerPos, stateMachine);
                    }
                }
            }
        }
    }
}