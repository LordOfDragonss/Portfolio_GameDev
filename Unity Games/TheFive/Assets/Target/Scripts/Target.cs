using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    [SerializeField] HealthSystem health;
    [SerializeField] Canvas damagedisplay;
    [SerializeField] Text TotaldamageText;
    [SerializeField] Text DamageText;
    [SerializeField] int CombotrackerTimer;
    int totalhealthlost;

    private void Awake()
    {
        health = GetComponent<HealthSystem>();
    }

    private void Update()
    {
        CombotrackerTimer--;
        DisplayDamage();
        if(CombotrackerTimer <= 0)
        {
            CombotrackerTimer = 0;
            health.currenthealth = health.maxhealth;
            totalhealthlost = 0;
            DamageText.text = "0";
        }
    }

    void DisplayDamage()
    {
        TotaldamageText.text = totalhealthlost.ToString();
        if (health.Damagetaken > 0)
        {
            DamageText.text = health.Damagetaken.ToString();
        }



        if (health.currenthealth < health.maxhealth)
        {
            totalhealthlost = health.maxhealth - health.currenthealth;
        }
    }

}
