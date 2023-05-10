using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public static WeaponHandler instance;
    public static weapons WeaponEquiped;
    public enum weapons
    {
        none,
        swordnshield,
        hammer,
        bow,
        spear,
        dagger
    }



    [SerializeField] Sword sword;
    [SerializeField] Shield shield;
    [SerializeField] GameObject hammer;
    [SerializeField] GameObject bow;
    [SerializeField] GameObject spear;
    [SerializeField] GameObject dagger;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        sword.gameObject.SetActive(false);
        hammer.gameObject.SetActive(false);
        bow.gameObject.SetActive(false);
        spear.gameObject.SetActive(false);
        dagger.gameObject.SetActive(false);
    }

    private void Update()
    {
        switch (WeaponEquiped)
        {
            case weapons.none:
                sword.gameObject.SetActive(false);
                shield.gameObject.SetActive(false);
                hammer.gameObject.SetActive(false);
                bow.gameObject.SetActive(false);
                spear.gameObject.SetActive(false);
                dagger.gameObject.SetActive(false);
                break;
            case weapons.swordnshield:
                sword.gameObject.SetActive(true);
                shield.gameObject.SetActive(true);
                hammer.gameObject.SetActive(false);
                bow.gameObject.SetActive(false);
                spear.gameObject.SetActive(false);
                dagger.gameObject.SetActive(false);
                break;
            case weapons.hammer:
                hammer.gameObject.SetActive(true);
                sword.gameObject.SetActive(false);
                shield.gameObject.SetActive(false);
                bow.gameObject.SetActive(false);
                spear.gameObject.SetActive(false);
                dagger.gameObject.SetActive(false);
                break;
            case weapons.bow:
                bow.gameObject.SetActive(true);
                shield.gameObject.SetActive(false);
                hammer.gameObject.SetActive(false);
                sword.gameObject.SetActive(false);
                spear.gameObject.SetActive(false);
                dagger.gameObject.SetActive(false);
                break;
            case weapons.spear:
                spear.gameObject.SetActive(true);
                shield.gameObject.SetActive(false);
                hammer.gameObject.SetActive(false);
                bow.gameObject.SetActive(false);
                sword.gameObject.SetActive(false);
                dagger.gameObject.SetActive(false);
                break;
            case weapons.dagger:
                dagger.gameObject.SetActive(true);
                shield.gameObject.SetActive(false);
                hammer.gameObject.SetActive(false);
                bow.gameObject.SetActive(false);
                spear.gameObject.SetActive(false);
                sword.gameObject.SetActive(false);
                break;
        }
    }

    public void Attack()
    {
        Animator anim = new();
        switch (WeaponEquiped)
        {
            case weapons.swordnshield:
                anim = sword.GetComponentInChildren<Animator>();
                break;
            case weapons.hammer:
                anim = hammer.GetComponent<Animator>();
                break;
            case weapons.bow:
                anim = hammer.GetComponent<Animator>();
                break;
            case weapons.spear:
                anim = hammer.GetComponent<Animator>();
                break;
            case weapons.dagger:
                anim = hammer.GetComponent<Animator>();
                break;
        }
        if (anim != null)
            anim.SetTrigger("Attack");
    }

    public void Ability1()
    {
        Animator anim = new();
        switch (WeaponEquiped)
        {
            case weapons.swordnshield:
                anim = sword.GetComponent<Animator>();
                if (shield.currentstate == Shield.shieldstate.idle) shield.currentstate = Shield.shieldstate.block;
                else if (shield.currentstate == Shield.shieldstate.block) shield.currentstate = Shield.shieldstate.idle;
                break;
            case weapons.hammer:
                anim = hammer.GetComponent<Animator>();
                break;
            case weapons.bow:
                anim = hammer.GetComponent<Animator>();
                break;
            case weapons.spear:
                anim = hammer.GetComponent<Animator>();
                break;
            case weapons.dagger:
                anim = hammer.GetComponent<Animator>();
                break;
        }
        if (anim != null)
            anim.SetTrigger("Ability1");
    }
    public void Ability2()
    {
        Animator anim = new();
        switch (WeaponEquiped)
        {
            case weapons.swordnshield:
                anim = sword.GetComponent<Animator>();
                sword.FireStrike();
                break;
            case weapons.hammer:
                anim = hammer.GetComponent<Animator>();
                break;
            case weapons.bow:
                anim = hammer.GetComponent<Animator>();
                break;
            case weapons.spear:
                anim = hammer.GetComponent<Animator>();
                break;
            case weapons.dagger:
                anim = hammer.GetComponent<Animator>();
                break;
        }
        if (anim != null)
            anim.SetTrigger("Ability2");
    }
    public void Ability3()
    {
        Animator anim = new();
        switch (WeaponEquiped)
        {
            case weapons.swordnshield:
                anim = sword.GetComponent<Animator>();
                break;
            case weapons.hammer:
                anim = hammer.GetComponent<Animator>();
                break;
            case weapons.bow:
                anim = hammer.GetComponent<Animator>();
                break;
            case weapons.spear:
                anim = hammer.GetComponent<Animator>();
                break;
            case weapons.dagger:
                anim = hammer.GetComponent<Animator>();
                break;
        }
        if (anim != null)
            anim.SetTrigger("Ability3");
    }

}
