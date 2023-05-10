using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevControl : MonoBehaviour
{
    [SerializeField] GameObject forceFinale;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            forceFinale.SetActive(true);
        }
    }
}
