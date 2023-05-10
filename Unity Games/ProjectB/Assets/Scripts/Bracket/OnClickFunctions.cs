using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickFunctions : MonoBehaviour
{
    [SerializeField] BracketLocation[] bracket;
    [SerializeField] Readfight read;
    [SerializeField] BracketSystem bracketSystem;

    public void spawnFightOnLocation()
    {
        Debug.Log("Spawning in");
        for (int i = 0; i < bracketSystem.fights.Count; i++)
        {
            Fight currentfight1 = read.getFight(i);
            Instantiate(currentfight1.firstFighter, bracket[i].spawn1.transform);
            Instantiate(currentfight1.secondFighter, bracket[i].spawn2.transform);
        }

    }

    public void spawnFightOne()
    {
        FightData.firstFighter = read.getFight(0).firstFighter;
        FightData.secondFighter = read.getFight(0).secondFighter;

        SceneManager.LoadScene("Level1");
    }

}
