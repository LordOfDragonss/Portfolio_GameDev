using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] Animator DeathAnimController;
    [SerializeField] Rigidbody RigidbodyE;
    [SerializeField] GameObject Vision;
    [SerializeField] Collider ColliderE;
    public void DeathSequence()
    {
        if (DeathAnimController != null)
            DeathAnimController.Play("Death");
        if (RigidbodyE != null)
            RigidbodyE.MoveRotation(Quaternion.Euler(0, 10, 0));
        if (Vision != null)
            Vision.SetActive(false);
        if (ColliderE != null)
            ColliderE.enabled = false;
    }
}
