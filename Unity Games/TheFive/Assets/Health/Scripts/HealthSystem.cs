using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] internal int currenthealth;
    [SerializeField] internal int maxhealth;
    public int Damagetaken;
    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxhealth;
    }

    public void GainHealth(int amount)
    {
        if(currenthealth < maxhealth && amount !> currenthealth -maxhealth)
        {
            currenthealth += amount;
        }
        else if(amount > currenthealth - maxhealth)
        {
            amount = currenthealth - maxhealth;
            currenthealth += amount;
        }
    }
    public void ReduceHealth(int amount)
    {
        Damagetaken = amount;
        if (currenthealth > 0 && amount < currenthealth)
        {
            currenthealth -= amount;
        }
        else if (amount >= currenthealth)
        {
            currenthealth = 0;
        }
    }
}
