using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    GameController gameController;
    public Transform respawnPoint;
    SpriteRenderer spriteRenderer;
    Collider2D coll;
    AudioManager audioManager;

  
    
        
    

    // Start is called before the first frame update
    private void Awake()
    {
        gameController=GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>();
        spriteRenderer=GetComponent<SpriteRenderer>();
        coll=GetComponent<Collider2D>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioManager.PlaySFX(audioManager.checkpoint);
            gameController.UpdateCheckpoint(respawnPoint.position);
            spriteRenderer.color = Color.red;
            coll.enabled = false;
        }
    }
}
