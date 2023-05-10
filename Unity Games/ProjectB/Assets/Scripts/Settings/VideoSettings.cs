using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class VideoSettings : MonoBehaviour
{


    private Dropdown qualityDropdown;
    private Dropdown textureDropdown;
    private Dropdown aaDropdown;

    [SerializeField] private TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    
    // Start is called before the first frame update
    void Start()
    {
        
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;
        int currentResolutionIndex = 0;


        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " +
                     resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width
                  && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();
    }

    // Update is called once per frame

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        Debug.Log(resolution);
    }

    public void QualityLevel(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
    public void SetTextureQuality(int textureIndex)
    {
        QualitySettings.globalTextureMipmapLimit = textureIndex;
        qualityDropdown.value = 6;
    }
    public void SetAntiAliasing(int aaIndex)
    {
        QualitySettings.antiAliasing = aaIndex;
        qualityDropdown.value = 4;
    }
}
