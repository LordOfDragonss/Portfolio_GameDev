using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCollision : MonoBehaviour
{
    [SerializeField] private Collider CollisionParent;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null && player.isFading)
            {
                CollisionParent.isTrigger = true;
            }
            else if (!player.isFading)
            {
                CollisionParent.isTrigger = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
