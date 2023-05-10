using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Boss : MonoBehaviour
{
    public bool startBoss = false;
    public int stages = 3;
    public int cooldown;
    public int currentstage = 0;
    [SerializeField] internal Enemy bossEnemyScript;
    [SerializeField] internal Canvas Victory;
    [SerializeField] List<GameObject> teleportSpots;

    // Start is called before the first frame update
    void Start()
    {
        bossEnemyScript.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startBoss)
        {
            currentstage = 1;
            startBoss = false;
        }
        switch (currentstage)
        {
            case 0:
                bossEnemyScript.hasPath = false;
                break;
            case 1:
                bossEnemyScript.hasPath = true;
                break;
            case 2:
                bossEnemyScript.hasPath = false;
                transform.localScale = new Vector3(2, 2, 2);
                BossTeleport();
                break;
        }

    }

    void BossTeleport()
    {
        if(cooldown > 0)
        cooldown--;
        if (teleportSpots != null && teleportSpots.Count > 0 && cooldown == 0)
        {
            Teleport(teleportSpots[Random.Range(0, teleportSpots.Count)]);
        }
    }

    internal void Teleport(GameObject tpTarget)
    {
        cooldown = 500;
        transform.position = tpTarget.transform.position;
        Physics.SyncTransforms();
    }
}
