using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject Location;
    [SerializeField] GameObject spawnObject;

    public float SpawnTimer;
    public float SpawnTime;
    public float timespeed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SpawnTimer+= timespeed;
        if (SpawnTimer >= SpawnTime)
        {
            Instantiate(spawnObject,Location.transform);
            SpawnTimer = 0;
        }
    }
}
