﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//This script keeps track of all controllers that enter or leave the trigger collider attached to this gameobject;
//It is used by 'MovingPlatform' to detect and move controllers standing on top of it;
public class TriggerAreaRotating : MonoBehaviour
{
    //check
    public List<Rigidbody> rigidbodiesInTriggerArea = new List<Rigidbody>();

    //Check if the collider that just entered the trigger has a rigidbody (and a mover) attached and add it to the list;
    void OnTriggerEnter(Collider col)
    {
        if (col.attachedRigidbody != null)
        {
            col.transform.SetParent(transform.parent, true);
        }
    }

    //Check if the collider that just left the trigger has a rigidbody (and a mover) attached and remove it from the list;
    void OnTriggerExit(Collider col)
    {
        if (col.attachedRigidbody != null)
        {
            col.transform.SetParent(null);
        }
    }
}
