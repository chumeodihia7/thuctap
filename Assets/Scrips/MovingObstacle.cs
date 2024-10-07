using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0,5)]
    public float speed;
   
    Vector3 targetPos;
    public GameObject ways;
    public Transform[] waysPoint;
    int pointIndex;
    int pointCount;
    int direction = 1;
    [Range(0, 2)]
    public float waitDuration;
    int speedMultiplier = 1;

    private void Awake()
    {
        waysPoint = new Transform[ways.transform.childCount];
        for (int i = 0; i < ways.gameObject.transform.childCount; i++)
        {
            waysPoint[i] = ways.transform.GetChild(i).gameObject.transform;
        }

    }
    void Start()
    {
        pointCount = waysPoint.Length;
        pointIndex = 1;
        targetPos = waysPoint[pointIndex].transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        var step = speedMultiplier*speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);

        if (transform.position == targetPos)

        {
            NextPoint();
        }
    }

    void NextPoint()
    {
        if (pointIndex == pointCount - 1) // Arrived last point
        {
            direction = -1;
        }

        if (pointIndex == 0) // Arrived first point
        {
            direction = 1;
        }

        pointIndex += direction;
        targetPos = waysPoint[pointIndex].transform.position;
        StartCoroutine(WaitNextPoint());

    }
     
    IEnumerator WaitNextPoint()
    {
        speedMultiplier = 0;
        yield return new WaitForSeconds(waitDuration);
        speedMultiplier = 1;
    }
}
