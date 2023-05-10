using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var player = other.GetComponent<Player>();
            player.healthSystem.GainHealth(2);
        }
    }
}
