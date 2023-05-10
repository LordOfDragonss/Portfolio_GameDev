using System;
using System.Collections;
using System.Collections.Generic;
using TwoDots.SimpleHealthSystem;
using UnityEngine;
using UnityEngine.Serialization;

public class Health : MonoBehaviour
{
    //[HideInInspector]
    public int CurrentHealth;

    public int MaxHealth = 100;

    internal HealthBar healthBar;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public void Heal(int add)
    {
        CurrentHealth = Math.Min(CurrentHealth + add, MaxHealth);
        SetHealthAtHealthBar();
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth = Math.Max(CurrentHealth - damage, 0);

        if (CurrentHealth == 0)
        {
            // You Died
        }
        
        SetHealthAtHealthBar();
    }

    private void SetHealthAtHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.SetNewHealth((float)CurrentHealth / MaxHealth);
        }
    }
}
