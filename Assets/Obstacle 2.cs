using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed;
    public GameObject ways;
    public Transform[] wayPoints;
    private int pointIndex;
    private int direction = 1;
    private Vector3 targetPos;

    private void Awake()
    {
        wayPoints = new Transform[ways.transform.childCount];
        for (int i = 0; i < ways.transform.childCount; i++)
        {
            wayPoints[i] = ways.transform.GetChild(i);
        }
    }

    private void Start()
    {
        pointIndex = 0;
        targetPos = wayPoints[pointIndex].position;
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);

        if (Vector3.Distance(transform.position, targetPos) < 0.001f)
        {
            NextPoint();
        }
    }

    private void NextPoint()
    {
        pointIndex += direction;
        
        if (pointIndex >= wayPoints.Length || pointIndex < 0)
        {
            direction *= -1;
            pointIndex += direction;
        }

        targetPos = wayPoints[pointIndex].position;
    }
}
