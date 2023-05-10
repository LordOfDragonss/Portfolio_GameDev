using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HealMove : Move
{
    public GameObject healthParticleSystem;


    private GameObject psInstance;
    


    public override void OnAnimationStart(CombatEntity context)
    {
        psInstance = Instantiate(healthParticleSystem, context.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        base.OnAnimationStart(context);
    }

    public override void Execute(CombatEntity context)
    {
        context.health.Heal(Damage);
    }

    public override void OnCancel(CombatEntity context)
    {
        OnAnimationEnd(context);
    }

    public override void OnAnimationEnd(CombatEntity context)
    {
        context.GetComponent<AudioSource>().Stop();
        base.OnAnimationEnd(context);
        Destroy(psInstance);
    }
}
