using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SlipperMove : Move
{
    public GameObject slipper;
    
    public override void Execute(CombatEntity context)
    {
        var spawnPos = context.HitCollider.transform.position;
        var projectile = Instantiate(slipper, spawnPos, Quaternion.identity).GetComponent<Projectile>();
        var direction = spawnPos.x > context.transform.position.x ? 1 : -1;   
        projectile.Init(Damage, direction, context);
    }
}
    