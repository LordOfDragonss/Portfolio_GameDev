using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.Audio;
public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private string audioName;
    // Start is called before the first frame update
   public void SetVolume(Slider slider)
    {
        
        mixer.SetFloat(audioName, Mathf.Log10(slider.value) * 10);
    }
}
