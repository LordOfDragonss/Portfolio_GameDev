using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public bool rotate;
    public float rotationSpeed;
    public float lifeTime;
    public float initialVelY;

    private int damage;
    private CombatEntity owner;
    private Rigidbody2D rigidbody;
    private AudioSource audio;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
    }

    public void Init(int damage, float directionX, CombatEntity owner)
    {
        this.damage = damage;
        this.owner = owner;

        Destroy(gameObject, lifeTime);
        rigidbody.AddForce(new Vector2(speed * directionX, initialVelY));
        if (rotate)
            rigidbody.AddTorque(rotationSpeed * directionX);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {

        if (col.collider.TryGetComponent<CombatEntity>(out var other))
        {
            if (other != owner)
            {
                other.health.TakeDamage(damage);
                damage = 0;
            }
        }
        //else
        //damage = 0;
        if (damage == 0)
        {
            audio.Stop();
            Destroy(gameObject, 1f);
        }

    }
}