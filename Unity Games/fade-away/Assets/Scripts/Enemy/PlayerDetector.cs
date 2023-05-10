using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public Canvas gameover;
    private void OnTriggerEnter(Collider other)
    {
        if (transform.parent.gameObject.tag == "Target")
        {
            if (other.gameObject.tag == "Player")
            {
                Cursor.lockState = CursorLockMode.Confined;
                Time.timeScale = 0;
                gameover.enabled = true;
            }
        }
        if (transform.parent.gameObject.tag == "Boss")
        {
            if (other.gameObject.tag == "Player")
            {
                var boss = transform.parent.gameObject.GetComponent<Boss>();
                boss.currentstage = 0;

                var player = other.GetComponent<Player>();
                if (player.IsGrounded)
                {
                    player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 10, player.transform.position.z);
                }
                player.canMove = false;
                if (player.Souls == 0)
                {
                    Cursor.lockState = CursorLockMode.Confined;
                    Time.timeScale = 0;
                    gameover.enabled = true;
                }
                Debug.Log("BOSS HAS SEEN PLAYER");
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (transform.parent.gameObject.tag == "Boss")
        {
            if (other.gameObject.tag == "Player")
            {

                var player = other.GetComponent<Player>();
                player.Souls -= 0.1f;
                player.canMove = false;

                if (player.Souls <= 0)
                {
                    Cursor.lockState = CursorLockMode.Confined;
                    Time.timeScale = 0;
                    gameover.enabled = true;
                }

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (transform.parent.gameObject.tag == "Boss")
        {
            if (other.gameObject.tag == "Player")
            {
                var boss = transform.parent.gameObject.GetComponent<Boss>();
                boss.currentstage = 1;
                var player = other.GetComponent<Player>();
                player.canMove = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
