using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightData : MonoBehaviour
{
    public static GameObject firstFighter;
    public static int Stage;
    public static GameObject secondFighter;
    static FightData fightData;
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    void Start()
    {
        firstFighter = BracketSystem.instance.fights[0].firstFighter;
        secondFighter = BracketSystem.instance.fights[0].secondFighter;
    }
}
