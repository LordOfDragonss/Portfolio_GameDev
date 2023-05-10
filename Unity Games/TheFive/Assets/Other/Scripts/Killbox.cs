using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.position = Vector3.zero;
            return;
        }
        if (other.gameObject.layer != LayerMask.NameToLayer("Undestroyable"))
        Destroy(other.gameObject);

    }
}
