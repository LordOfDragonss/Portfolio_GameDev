﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//It also rotates any controllers on top along with it;
public class RotatingPlatform : MonoBehaviour
{
    //References to attached components;
    Rigidbody r;
    TriggerArea triggerArea;
     Vector3 rotationAmount = Vector3.zero;

    //Start;
    void Awake()
    {
        //Get references to components;
        r = GetComponent<Rigidbody>();
        triggerArea = GetComponentInChildren<TriggerArea>();

        //Disable gravity, freeze rotation of rigidbody and set to kinematic;
        r.freezeRotation = true;
        r.useGravity = false;
        //r.isKinematic = true;
        rotationAmount = r.rotation.eulerAngles;
    }
    private void FixedUpdate()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        if (triggerArea == null)
            return;

        //Move all controllrs on top of the platform the same distance;

        foreach (Rigidbody rb in triggerArea.rigidbodiesInTriggerArea)
        {
            Vector3 offsetRot = r.rotation.eulerAngles - rotationAmount;
            Vector3 moveVector = RotatePointAroundPivot(rb.transform.position, transform.position, offsetRot);
            rb.MovePosition(moveVector);
        }
        rotationAmount = r.rotation.eulerAngles;
    }

    public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        Vector3 dir = point - pivot; // get point direction relative to pivot
        dir = Quaternion.Euler(angles) * dir; // rotate it
        point = dir + pivot; // calculate rotated point
        return point; // return it
    }
}