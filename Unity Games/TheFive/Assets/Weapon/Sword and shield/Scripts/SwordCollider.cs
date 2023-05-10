using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollider : MonoBehaviour
{
    [SerializeField] Sword sword;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("SwordAttack"))
            {
                var enemyHP = other.gameObject.GetComponent<HealthSystem>();
                enemyHP.ReduceHealth(sword.DamageAttack);
            }
        }
    }
}
