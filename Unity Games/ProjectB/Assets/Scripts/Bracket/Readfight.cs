using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Readfight : MonoBehaviour
{
    [SerializeField] int FightToSpawn;
    Fight currentfight;
    [SerializeField] BracketSystem bracketSystem;
    [SerializeField] OnClickFunctions clickFunctions;
    private void Start()
    {
        StartCoroutine(moveToFight());
        Debug.Log(FightData.Stage);

    }
    private void Update()
    {

    }

    IEnumerator moveToFight()
    {
        yield return new WaitForSeconds(0.1f);
        clickFunctions.spawnFightOnLocation();

        yield return new WaitForSeconds(3f);
        if (FightData.Stage == 0)
        {
            spawnFightOne();
        }
        if (FightData.Stage == 1)
        {
            bracketSystem.fights[0].winner = 1;
            bracketSystem.fights[1].winner = 1;

            //dev check code for fight 2
            bracketSystem.fights[2].winner = 1;
            bracketSystem.fights[3].winner = 1;

            advanceBranch(bracketSystem.fights[0], bracketSystem.fights[1]);
            advanceBranch(bracketSystem.fights[2], bracketSystem.fights[3]);
            clickFunctions.spawnFightOnLocation();
            yield return new WaitForSeconds(3f);
            spawnFightTwo();
        }
        if (FightData.Stage == 2)
        {

            bracketSystem.fights[0].winner = 1;
            bracketSystem.fights[1].winner = 1;

            //dev check code for fight 2
            bracketSystem.fights[2].winner = 1;
            bracketSystem.fights[3].winner = 1;

            advanceBranch(bracketSystem.fights[0], bracketSystem.fights[1]);
            advanceBranch(bracketSystem.fights[2], bracketSystem.fights[3]);

            bracketSystem.fights[4].winner = 1;
            bracketSystem.fights[5].winner = 1;

            advanceBranch(bracketSystem.fights[4], bracketSystem.fights[5]);
            clickFunctions.spawnFightOnLocation();
            yield return new WaitForSeconds(3f);
            spawnFightThree();
        }
    }

    public void spawnFightOne()
    {
        //FightData.firstFighter = getFight(0).firstFighter;
        //FightData.secondFighter = getFight(0).secondFighter;

        SceneManager.LoadScene("Level1");
    }

    public void spawnFightTwo()
    {
        //FightData.firstFighter = getFight(4).firstFighter;
        //FightData.secondFighter = getFight(4).secondFighter;

        SceneManager.LoadScene("Level2");
    }

    public void spawnFightThree()
    {
        //FightData.firstFighter = getFight(7).firstFighter;
        //FightData.secondFighter = getFight(7).secondFighter;
        FightData.Stage = 0;

        SceneManager.LoadScene("End Screen");
    }

    public Fight getFight(int fightNr)
    {
        return bracketSystem.fights[fightNr];
    }
    void advanceBranch(Fight firstFight, Fight secondFight)
    {
        if (firstFight.winner != 0 && firstFight.winner != 0)
        {
            GameObject winnerFirstFight = null;
            GameObject winnerSecondFight = null;
            if (firstFight.winner == 1)
                winnerFirstFight = firstFight.firstFighter;
            else if (bracketSystem.fights[0].winner == 2)
                winnerFirstFight = firstFight.secondFighter;

            if (secondFight.winner == 1)
                winnerSecondFight = secondFight.firstFighter;
            else if (secondFight.winner == 2)
                winnerSecondFight = secondFight.secondFighter;

            bracketSystem.fights.Add(new Fight(winnerFirstFight, winnerSecondFight));
        }



    }
}
