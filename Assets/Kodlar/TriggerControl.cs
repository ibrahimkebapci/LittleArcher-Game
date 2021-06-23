using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerControl : MonoBehaviour
{
    [SerializeField] GameObject player;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.GetComponent<PlayerController>().onGround = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        player.GetComponent<PlayerController>().onGround = false;  
    }
}
