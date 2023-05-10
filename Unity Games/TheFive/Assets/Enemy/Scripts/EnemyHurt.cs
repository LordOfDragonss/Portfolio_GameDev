using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurt : MonoBehaviour
{
    [SerializeField] HealthSystem healthSystem;
    [SerializeField] EnemyDeath Death;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(healthSystem.currenthealth > healthSystem.maxhealth)
        {
            healthSystem.currenthealth = healthSystem.maxhealth;
        }
        if(healthSystem.currenthealth <= 0)
        {
            Death.DeathSequence();
        }
    }
}
