using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTarget : MonoBehaviour
{
    public EnemyChase chase;
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        chase.Chase(other.gameObject);
    }
}
