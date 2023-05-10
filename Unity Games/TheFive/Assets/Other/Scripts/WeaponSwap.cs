using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwap : MonoBehaviour
{
    [SerializeField] Player player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            player.EquipedWeapon = WeaponHandler.weapons.swordnshield;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            player.EquipedWeapon = WeaponHandler.weapons.dagger;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            player.EquipedWeapon = WeaponHandler.weapons.hammer;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            player.EquipedWeapon = WeaponHandler.weapons.spear;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            player.EquipedWeapon = WeaponHandler.weapons.bow;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            player.EquipedWeapon = WeaponHandler.weapons.none;
        }
    }
}
