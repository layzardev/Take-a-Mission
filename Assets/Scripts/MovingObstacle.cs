using System;
using System.Collections;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [Range(0,5)]
    public float speed;

    [Range(0,2)]
    public float waitDuration;

    Vector3 targetPos;

    public GameObject ways;
    public Transform[] wayPoint;
    int pointIndex;
    int pointCount;
    int direction = 1;

    
    int speedMultiplier = 1;

    private void Awake()
    {
        wayPoint = new Transform[ways.transform.childCount];
        for (int i = 0; i <  ways.gameObject.transform.childCount; i++)
        {
            wayPoint[i] = ways.transform.GetChild(i).gameObject.transform;
        }
    }

    private void Start()
    {
        pointCount = wayPoint.Length;
        pointIndex = 1;
        targetPos = wayPoint[pointIndex].transform.position;
    }

    private void Update()
    {
        var step = speedMultiplier * speed * Time.deltaTime;
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
        targetPos = wayPoint[pointIndex].transform.position;
        StartCoroutine(WaitNextPoint());
    }

    IEnumerator WaitNextPoint()
    {
        speedMultiplier = 0;
        yield return new WaitForSeconds(waitDuration);
        speedMultiplier = 1;
    }
}
