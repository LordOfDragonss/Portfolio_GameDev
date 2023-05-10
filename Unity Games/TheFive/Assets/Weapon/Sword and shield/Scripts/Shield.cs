using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField]  Player player;
    [SerializeField]  int SlowedSpeed;
    [SerializeField] Animator animator;
    public enum shieldstate
    {
        idle,
        block
    }
    public shieldstate currentstate;

    private void Update()
    {
        if(currentstate == shieldstate.idle)
        {
            animator.SetBool("Blocking", false);
            player.movementSpeed = 10;
        }
        else if(currentstate == shieldstate.block)
        {
            animator.SetBool("Blocking", true);
            player.movementSpeed = SlowedSpeed;
        }

    }
}
