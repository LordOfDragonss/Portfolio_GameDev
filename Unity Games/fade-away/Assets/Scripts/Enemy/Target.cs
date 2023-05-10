using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public bool IsActive;
    [SerializeField] public ParticleSystem blood;
    [SerializeField] bool hasAbility;
    [SerializeField] string abilityHolding;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasAbility && abilityHolding != null)
        {
            blood.gameObject.SetActive(true);
        }
        else
        {
            blood.gameObject.SetActive(false);
        }
    }

    public void GiveAbility(Player player)
    {

       player.ac.ActivateAbility(abilityHolding);
    }

    public void Death(Player killer)
    {
        if (hasAbility)
        {
            GiveAbility(killer);
            if(abilityHolding != "Resource")
            TargetTracker.TargetsKilled++;
        }
        Destroy(gameObject);
    }
}
