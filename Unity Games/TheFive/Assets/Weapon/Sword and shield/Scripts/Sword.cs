using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public int DamageAttack;

    [SerializeField] Player player;
    [SerializeField] GameObject fireStrike;
    public void FireStrike()
    {
        Instantiate(fireStrike.gameObject,player.gameObject.transform.position, player.transform.rotation);
    }
}
