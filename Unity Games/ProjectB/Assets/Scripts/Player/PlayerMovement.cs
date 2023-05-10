using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Unity.VisualScripting;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] private float movementSpeed;
    public float _movementSpeed
    {
        get { return movementSpeed; }
    }
    private float inputValue;
    [SerializeField] private float jumpForce;
    private bool isGrounded = true;

    private bool facingRight;

    public bool IsGrounded
    {
        get { return isGrounded; }
    }

    Transform groundPoint;
    Transform character;
    Animator animator;
    MoveSet moveSet;

    bool repressedMove;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundPoint = transform.Find("Groundpoint");
        character = transform.Find("character");
        animator = character.GetComponent<Animator>();
        moveSet = GetComponent<MoveSet>();
    }

    private void Update()
    {
        handleFeet();
    }

    private void FixedUpdate()
    {
        bool walking = false;
        bool cancel = false;
        if (inputValue != 0)
        {
            if (moveSet.IsExecutingAttack)
            {
                if (!moveSet.currentMove.ForceStandStill)
                {
                    walking = true;
                }
                else if (moveSet.currentMove.IsCancelable)
                {
                    walking = true;
                    cancel = true;
                }
            }
            else
            {
                walking = true;
            }
        }

        if (cancel)
        {
            if (repressedMove)
            {
                moveSet.CancelMove();
                animator.Play("Run", 0);
                animator.Play("Run", 1);
            }
            else
            {
                walking = false;
            }
        }
        animator.SetBool("is_walking", walking);

        if (walking)
        {
            facingRight = inputValue < 0;
            float rotation = facingRight ? -90 : 90;

            character.eulerAngles = new Vector3(transform.eulerAngles.x, rotation, transform.eulerAngles.z);
            rb.AddForce(new Vector2(inputValue * movementSpeed, 0));
        }

        repressedMove = false;
    }


    /// <summary>
    /// function that handles the feet raycast
    /// </summary>
    private void handleFeet(){
        isGrounded = false;
        var hits = Physics2D.CircleCastAll((Vector2) groundPoint.position, 0.15f, Vector2.zero, 0);
       foreach(var hit in hits){

            if(hit.collider.gameObject == gameObject)
                continue;

        
        if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                isGrounded = true;



            if(hit.collider.gameObject.layer == LayerMask.NameToLayer("CombatEntity"))
                push(hit.collider.transform);
        }
    }

    private void push(Transform other){
        float dir = transform.position.x > other.position.x ? 10 : -10;
        Debug.Log("PUSH");
        rb.AddForce(new Vector2(dir, 0));
    }

    public void HandleMovement(InputAction.CallbackContext context)
    {
        if (Time.timeScale == 0)
            return;
        var oldInput = inputValue;
        inputValue = context.ReadValue<float>();
        if (oldInput == 0 && inputValue != 0)
        {
            repressedMove = true;
        }
    }


    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.started || !isGrounded || Time.timeScale == 0 || rb.velocity.y > 0)
            return;

        rb.AddForce(new Vector2(0, jumpForce));
    }

}