using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 checkpointPos ;
    Rigidbody2D playerRb;
    public ParticleControl particleController;
    public Transform playerTransform;
    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>(); 
    }
    void Start()
    {
        checkpointPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Die();
        }
    }
    public void UpdateCheckpoint( Vector2 pos)
    {
        checkpointPos =pos;
    }

    void Die()
    {
     
        StartCoroutine(Respawn(0.7f));
    }
    // Update is called once per frame

    IEnumerator Respawn(float duration)
    {
        particleController.PlayDieParticle(playerTransform.position);
        playerRb.simulated = false;
        playerRb.velocity = new Vector2(0,0);
       /* yield return new WaitForSeconds(0.2f); // Cho phép hiệu ứng die phát trước khi respawn*/
        transform.localScale = new Vector3(0,0,0);
        yield return new WaitForSeconds(duration);
        transform.position =checkpointPos;
        transform.localScale = new Vector3(1,1,1);
        playerRb.simulated = true;
    }
    void Update()
    {
        
    }
}
