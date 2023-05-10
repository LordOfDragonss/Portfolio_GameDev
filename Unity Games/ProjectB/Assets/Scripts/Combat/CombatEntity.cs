using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class CombatEntity : MonoBehaviour
{
    public GameObject HitCollider;
    [Header("Debug")] 
    public bool drawGizmos;

    internal Animator animator;
    internal Health health;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        health = GetComponent<Health>();
    }

    public bool TestHit(out CombatEntity other)
    {
        var hits = Physics2D.CircleCastAll((Vector2) HitCollider.transform.position, HitCollider.transform.localScale.x, Vector2.zero, 0, LayerMask.GetMask("CombatEntity"));
        
        other = null;
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.TryGetComponent<CombatEntity>(out var entity))
            {
                if (entity != this)
                    other = entity;
            }
        }

        return other != null;
    }

    private void OnDrawGizmos()
    {
        if (drawGizmos)
            Gizmos.DrawWireSphere(HitCollider.transform.position, HitCollider.transform.localScale.x);
    }
}
