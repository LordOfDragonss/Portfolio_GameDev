using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //it uses right here instead of forward because of the rotation being based around the y axis and not z
        this.gameObject.transform.position += transform.right   * Time.deltaTime * 10f;
    }
}
