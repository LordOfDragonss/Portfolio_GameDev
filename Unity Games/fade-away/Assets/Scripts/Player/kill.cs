using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class kill : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Player player;
    PlayerInput playerInput;
    InputAction killAction;
    Boss boss;
    bool hasSeenBoss;
    void Start()
    {
        playerInput = player.GetComponent<PlayerInput>();
        killAction = playerInput.actions["kill"];
    }
    public Text Killtext;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Target")
        {
            if (Killtext != null)
            {
                Killtext.enabled = true;
                Destroy(Killtext, 3f);
            }
        }
        if (other.gameObject.tag == "Boss")
        {
            Debug.Log("boss murder");
            boss = other.gameObject.GetComponent<Boss>();
            hasSeenBoss = true;
        }
    }

    public Text errorText;
    private void OnTriggerStay(Collider other)
    {
        if (killAction.IsPressed())
        {
            //isPressed = false
            if (other.gameObject.tag == "Target")
            {
                Target target = other.gameObject.GetComponent<Target>();
                if (target.IsActive)
                {
                    player.audioHandler.PlayStabSound();
                    player.Souls += 10;
                    target.IsActive = false;
                    target.blood.gameObject.SetActive(true);
                    target.Death(player);
                }

                else
                {
                    errorText.text = "You can't kill this";
                }
            }
            if (other.gameObject.tag == "Boss")
            {
                boss = other.gameObject.GetComponent<Boss>();
                hasSeenBoss = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Boss")
        {
            Debug.Log("boss murder");
            hasSeenBoss = false;
        }
    }

    //void AssignNewTarget()
    //{
    //    GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
    //    int newtarget = Random.Range(0, targets.Length);
    //    Target target = targets[newtarget].GetComponent<Target>();
    //    if (target != null && !target.IsActive)
    //        target.IsActive = true;
    //    else
    //    {
    //        AssignNewTarget();
    //    }


    //}

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(hasSeenBoss);
        if (killAction.WasPressedThisFrame() && hasSeenBoss)
        {
            hasSeenBoss = false;
            if (boss.currentstage < boss.stages)
            {
                boss.currentstage++;
            }
            if(boss.currentstage == 2)
            {
                player.audioHandler.SoundtrackGame.pitch = 2f;
            }
            if (boss.currentstage == boss.stages)
            {
                Cursor.lockState = CursorLockMode.Confined;
                boss.Victory.enabled = true;
                Time.timeScale = 0;
                TargetTracker.TargetsKilled++;
            }
        }
    }
}
