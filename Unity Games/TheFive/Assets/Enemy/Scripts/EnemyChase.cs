using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public int ChaseSpeed;
    public void Chase(GameObject target)
    {
        Vector3 distance = target.transform.position - transform.position;
        distance.Normalize();
        transform.position += distance * ChaseSpeed * Time.deltaTime;

    }
}
