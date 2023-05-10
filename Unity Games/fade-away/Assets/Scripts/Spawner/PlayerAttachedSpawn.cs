using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttachedSpawn : MonoBehaviour
{
    public Spawner spawner;
    Player attachedPlayer;

    private void Awake()
    {
        attachedPlayer = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attachedPlayer.Souls <= 0 && spawner.spawnTimer == 0)
        {
            spawner.Active = true;
            spawner.spawnTimer = 1000;
            
        }
        else if(spawner.spawnTimer > 0)
        {
            spawner.Active = false;
        }
        if(attachedPlayer.Souls > 0)
        {
            spawner.Active = false;
        }
    }
}
