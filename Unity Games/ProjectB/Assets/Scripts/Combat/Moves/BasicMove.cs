using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BasicMove : Move
{
    [SerializeField] private AudioClip[] missSfx;
    public override void Execute(CombatEntity context)
    {
        if (context.TestHit(out var other))
        {
            Debug.Log("register Hit");
            other.health.TakeDamage(Damage);
            if(sfx.Length == 0)
                return;
            context.GetComponent<AudioSource>().PlayOneShot(sfx[Random.Range(0,sfx.Length)]);
        }else{
            if(missSfx.Length == 0)
                return;
            context.GetComponent<AudioSource>().PlayOneShot(missSfx[Random.Range(0,missSfx.Length)]);
        }
    }

    public override void OnAnimationEnd(CombatEntity context)
    {
    }

    public override void OnAnimationStart(CombatEntity context)
    {
    }
}
