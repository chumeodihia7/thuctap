using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform destination;
    GameObject player;
    Animation anim;
    Rigidbody2D playerRB;

    AudioManager audioManager;
    void Start()
    {
        
    }
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = player.GetComponent<Animation>();
        playerRB=player.GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(Vector2.Distance(player.transform.position,transform.position) > 0.3f)
            {
                StartCoroutine(PortalIn());
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator PortalIn()
    {
        audioManager.PlaySFX(audioManager.protalIn);
        playerRB.simulated = false;
        anim.Play("Go");
        StartCoroutine(MoveInPortal());
        yield return new WaitForSeconds(0.8f);
        player.transform.position = destination.position;
        playerRB.velocity = Vector2.zero;
        anim.Play("Out");
        audioManager.PlaySFX(audioManager.protalOut);
        yield return new WaitForSeconds(0.8f);
        playerRB.simulated = true;
    }

    IEnumerator MoveInPortal()
    {
        float timer = 0;
        while (timer < 0.8f)
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, transform.position, 3 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
    }
}
