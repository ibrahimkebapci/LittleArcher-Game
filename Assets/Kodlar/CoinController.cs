using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinController : MonoBehaviour
{
    private string Score = "Score: " ;
    [SerializeField] Text scoreValueText;
    [SerializeField] float CoinRotateSpeed;

    private void Update()
    {
        transform.Rotate(new Vector3(0f,CoinRotateSpeed, 0f));
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            DBManager.score = DBManager.score + 50;
            scoreValueText.text =  Score + DBManager.score.ToString();
            Destroy(gameObject);
        }
    }
}
