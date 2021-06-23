using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public Sprite whiteTree;
    public Sprite yellowTree;
    private SpriteRenderer checkpointeRenderer;
    public bool checkpointReached;
    private GameMaster gm;

    // Start is called before the first frame update
    void Start()
    {
        checkpointeRenderer = GetComponent<SpriteRenderer>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gm.lastCheckPointPos = transform.position;
            checkpointeRenderer.sprite = yellowTree;
            checkpointReached = true;
        }
    }
}
