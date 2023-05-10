using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    
    [SerializeField] AudioHandler audioHandler;
    private void Awake()
    {
        audioHandler.SoundtrackMainmenu.Play();
    }
    public void StartGame()
    {
        audioHandler.SoundtrackMainmenu.Stop();
        SceneManager.LoadScene("MainWorld");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
