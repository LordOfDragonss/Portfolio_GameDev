using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public int time = 0;
    public int starttime;
    public bool disable;
    // Start is called before the first frame update
    void Start()
    {
        time = starttime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time--;
        if (disable && time==0)
        {
            DisableAfterTime();
        }
        if (time <= 0)
        {
            time = 0;
        }
    }

    void DisableAfterTime()
    {
        gameObject.SetActive(false);
    }
}
