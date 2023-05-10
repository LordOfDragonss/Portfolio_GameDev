using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartFight : MonoBehaviour
{
    [SerializeField] GameObject firstspawn;
    [SerializeField] GameObject secondspawn;
    [SerializeField] TextMeshProUGUI Countdown;
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(FightData.firstFighter, firstspawn.transform);
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        Time.timeScale = 0;
        Countdown.text = "3";
        yield return new WaitForSecondsRealtime(1f);
        Countdown.text = "2";
        yield return new WaitForSecondsRealtime(1f);
        Countdown.text = "1";
        yield return new WaitForSecondsRealtime(1f);
        Countdown.text = "";
        Time.timeScale = 1;
        //Instantiate(FightData.secondFighter, secondspawn.transform);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
