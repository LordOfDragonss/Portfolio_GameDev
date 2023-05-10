using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStrikeCollision : MonoBehaviour
{
    [SerializeField] int Damage;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            var enemyHP = other.gameObject.GetComponent<HealthSystem>();
            enemyHP.ReduceHealth(Damage);
        }
    }
}
