using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwapSceneCollision : MonoBehaviour
{
    [SerializeField] string SceneName;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Scene scene = SceneManager.GetSceneByName(SceneName);
            SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
            SceneManager.MoveGameObjectToScene(other.gameObject, scene);


        }
    }
}
