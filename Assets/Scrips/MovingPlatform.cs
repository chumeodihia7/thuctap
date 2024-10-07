using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
   
    public float speed;
    Vector3 targetPos;

    MovementController movecontroller;
    Rigidbody2D rb;
    Rigidbody2D playerRb;
    Vector3 moveDirection;
    public GameObject Player;

    public GameObject Ways;
    public Transform[] wayPoint;
    int pointIndex;
    int pointCount;
    int direction = 1;

    public float waitDuration;

    private void Awake()
    {
        movecontroller = Player.GetComponent<MovementController>();
        playerRb= Player.GetComponent<Rigidbody2D>();   
        rb=GetComponent<Rigidbody2D>();

        wayPoint = new Transform[Ways.transform.childCount];
        for(int i = 0; i < Ways.gameObject.transform.childCount; i++)
        {
            wayPoint[i]=Ways.transform.GetChild(i).gameObject.transform;
        }
    }
    void Start()
    {
        pointIndex = 1;
        pointCount=wayPoint.Length;
        targetPos = wayPoint[1].transform.position;
        DirectionCalculate();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, targetPos) < 0.05f)
        {
            NextPoint();
        }
       
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDirection * speed;
    }

    void NextPoint()
    {
        transform.position = targetPos;
        moveDirection = Vector3.zero;
        if(pointIndex == pointCount-1)
        {
            direction = -1;
        }

        if(pointIndex==0)
        {
            direction=1;
        }
        pointIndex += direction;
        targetPos = wayPoint[pointIndex].transform.position;

        StartCoroutine(WaitNextPoint());
    }

    IEnumerator WaitNextPoint()
    {
        yield return new WaitForSeconds(waitDuration);
        DirectionCalculate();
    }
    void DirectionCalculate()
    {
        moveDirection = (targetPos-transform.position).normalized;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            movecontroller.isOnPlatform = true;
            movecontroller.platformRb = rb;
            playerRb.gravityScale=playerRb.gravityScale*50;

        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            movecontroller.isOnPlatform = false;
            playerRb.gravityScale = playerRb.gravityScale / 50;

        }
    }
}
