using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] GameObject effect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(collision.gameObject.tag == "Player") && !(collision.gameObject.tag =="BoundaryBox") 
            && !(collision.gameObject.tag == "coin") && !(collision.gameObject.tag == "CheckPoint"))
        {
            
            Destroy(gameObject);
            if(collision.gameObject.CompareTag("Enemy"))
            {
                Instantiate(effect, collision.gameObject.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
            }
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
