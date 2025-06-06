using UnityEngine;

public class FlyPath : MonoBehaviour
{
    public Transform[] pathPoints;
    public float speed = 3f;
    private int currentPointIndex = 0;

    void Update()
    {
        if (pathPoints.Length == 0) return;

        Transform target = pathPoints[currentPointIndex];
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            currentPointIndex++;

            if (currentPointIndex >= pathPoints.Length)
            {
                Destroy(gameObject);
            }
        }
    }
}