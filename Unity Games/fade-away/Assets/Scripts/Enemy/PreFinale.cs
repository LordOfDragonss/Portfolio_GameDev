using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreFinale : MonoBehaviour
{
    [SerializeField] AbilityController controller;
    [SerializeField] GameObject finalestarter;
    // Update is called once per frame
    void Update()
    {
        if (controller.npc1killed && controller.npc2killed)
        {
            finalestarter.SetActive(true);
        }
    }
}
