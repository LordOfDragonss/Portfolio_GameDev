using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableLeftZone : MonoBehaviour
{
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.ac.bossstarted)
        {
            gameObject.SetActive(false);
        }
    }
}
