using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject SpawnObject;
    [SerializeField] internal bool Active;
    [SerializeField] internal float spawnTimer = 0;

    private void Update()
    {
        if (Active)
        {
            Activate();
        }
        if(spawnTimer > 0)
        spawnTimer--;
    }

    public void Activate()
    {
        GameObject.Instantiate(SpawnObject, transform.position,transform.rotation);
    }

}
