using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for different enemy states
/// </summary>
public abstract class BaseState
{
    protected Animator anim; // Reference to the animator component
    protected Rigidbody2D rb; // Reference to the rigidbody2D component
    protected Transform trans; // Reference to the transform component
    public bool canTransition = true; // Flag to control state transitions

    public Transform playerPos; // Reference to the player's position

    public float attackRange = 1.1f; // Attack range of the enemy
    public float minSlipperRange = 1.5f;
    public float maxSlipperRange = 10.0f;
    public float safeHealDistance = 5f;
    public int healMinHealth = 50;
    public float movementSpeed; // Movement speed of the enemy

    public StateMachine stateMachine; // Reference to the state machine component
    public MoveSet moveSet; // Reference to the move set component


    /// <summary>
    /// Constructor for BaseState class
    /// </summary>
    /// <param name="animator">Reference to the animator component</param>
    /// <param name="rigidbody">Reference to the rigidbody2D component</param>
    /// <param name="transform">Reference to the transform component</param>
    /// <param name="playerposition">Reference to the player's position</param>
    /// <param name="statemachine">Reference to the state machine component</param>
    public BaseState(Animator animator, Rigidbody2D rigidbody, Transform transform, Transform playerposition, StateMachine statemachine)
    {
        anim = animator;
        rb = rigidbody;
        trans = transform;
        playerPos = playerposition;
        stateMachine = statemachine;
        movementSpeed = stateMachine.movementSpeed;
        moveSet = stateMachine.GetComponent<MoveSet>();

    }

    /// <summary>
    /// Function called when entering the state
    /// </summary>
    public virtual void OnEnter()
    {
        // Add code that should be executed when entering the state
    }

    /// <summary>
    /// Function called every frame while in the state
    /// </summary>
    public virtual void Update()
    {
    }

    /// <summary>
    /// Function called when exiting the state
    /// </summary>
    public virtual void OnExit()
    {
        // Add code that should be executed when exiting the state
    }

    /// <summary>
    /// Function called to switch to another state
    /// </summary>
    public virtual void SwitchState()
    {
        //if in idle or moving enemy can switch state
    }

}