using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRelay : MonoBehaviour
{
    private MoveSet moveSet;
    
    private void Awake()
    {
        moveSet = GetComponentInParent<MoveSet>();
    }

    public void OnAttackAnimationEvent()
    {
        moveSet.OnAttackAnimationEvent();
    }
}
