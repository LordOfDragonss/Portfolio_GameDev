using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Vector3 startPosition;
    public bool LetCameraFollow;
    [SerializeField] Player player;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(LetCameraFollow)
        transform.position = startPosition + player.transform.position;
        else
        {
            transform.position = startPosition;
        }
    }
}
