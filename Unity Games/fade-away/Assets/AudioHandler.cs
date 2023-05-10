using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    public AudioSource Button;
    public AudioSource Fade;
    public AudioSource Fadereverse;
    public AudioSource Teleport;
    public AudioSource Stab;
    public AudioSource SoundtrackMainmenu;
    public AudioSource SoundtrackGame;

    public void PlayButtonSound()
    {
        Button.Play();
    }
    public void PlayFadeSound()
    {
        Fade.Play();
    }
    public void PlayFadereverseSound()
    {
        Fadereverse.Play();
    }
    public void PlayTeleportSound()
    {
        Teleport.Play();
    }
    public void PlayStabSound()
    {
        Stab.Play();
    }

}
