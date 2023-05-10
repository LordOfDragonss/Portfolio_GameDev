using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Reset reset;
    public void LoadMainMenu()
    {
        reset.RestartGame();
        SceneManager.LoadScene("MainMenu");
    }
}
