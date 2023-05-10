using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeavingStartZone : MonoBehaviour
{
    [SerializeField] CameraFollow activecamera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            activecamera.LetCameraFollow = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            activecamera.LetCameraFollow = true;
        }
    }
}
