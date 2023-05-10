using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundeffects : MonoBehaviour
{
     public AudioSource randomSound;
 
     public AudioClip[] audioSources;
 
     // Use t$$anonymous$$s for initialization
     void Start () {
     }
 
     public void play()
     {
        randomSound.clip = audioSources[Random.Range(0, audioSources.Length)];
        randomSound.Play ();
     }
}
