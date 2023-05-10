using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BracketSystem : MonoBehaviour
{
    [SerializeField] int amount;
    [SerializeField] int rounds;
    public GameObject[] Enemies;
    public List<Fight> fights;
    public GameObject Player;
    public static BracketSystem instance;


    void Start()
    {
        instance = this;
        fights =  new List<Fight>();
        GenerateFights();
    }

    private void Update()
    {

    }



    void GenerateFights()
    {
        fights.Add(new Fight(Player, Enemies[0]));
        fights.Add(new Fight(Enemies[1], Enemies[2]));
        fights.Add(new Fight(Enemies[3], Enemies[4]));
        fights.Add(new Fight(Enemies[5], Enemies[6]));
        //fights.Add(new Fight(Enemies[7], Enemies[8]));
    }
}


