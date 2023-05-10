using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour
{
    public GameObject firstFighter;
    public GameObject secondFighter;

    public GameObject firstFighterIcon;
    public GameObject secondFighterIcon;

    public int winner;
    public Fight(GameObject firstFighter, GameObject secondFighter) {
        this.firstFighter = firstFighter;
        this.secondFighter = secondFighter;
    }


}
