using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum scenes{
    mainMenu = 0, Level1 = 2, GameOver = 3, Level2 = 4, End = 5
}

public class ChangeScene : MonoBehaviour
{
    static public bool blocked = false;
    [SerializeField] private scenes scene;


    /// <summary>
    /// switches scene to the set scene
    /// </summary>
    public void Switch(){
        if(blocked){
            
            return;
        }
        if(scene == scenes.mainMenu)
            FightData.Stage = 0;
        SceneManager.LoadScene((int)scene);
        blocked = true;


    }

    void Awake(){
        if(blocked)
            StartCoroutine(unblock());
    }

    IEnumerator unblock(){
        yield return new WaitForSecondsRealtime(0.2f);
        blocked = false;
    }

  
    public void SwitchScene(string name)
    {
        if(blocked)
            return;

        if(name ==  "Main Menu")
            FightData.Stage = 0;
        SceneManager.LoadScene(name);
    }

}
