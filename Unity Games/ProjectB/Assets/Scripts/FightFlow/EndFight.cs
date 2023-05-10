using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndFight : MonoBehaviour
{
    [SerializeField] Health playerHealth;
    [SerializeField] Health enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.CurrentHealth <= 0)
        {
            SceneManager.LoadScene("GameOverScene");
        }
        if (enemyHealth.CurrentHealth <= 0)
        {
            FightData.Stage++;
            SceneManager.LoadScene("BracketFull");
        }
    }
}
