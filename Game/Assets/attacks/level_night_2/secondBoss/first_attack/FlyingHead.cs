using UnityEngine;

public class FlyingHead : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 180f;
    public float lifetime = 5f;
    public float wiggleIntensity = 0.5f;
    public float wiggleFrequency = 5f;

    private Transform player;
    private float timer = 0f;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (player == null) return;

        timer += Time.deltaTime;

        Vector2 toPlayer = (player.position - transform.position).normalized;

        Vector2 wiggle = new Vector2(
            Mathf.PerlinNoise(timer * wiggleFrequency, 0f) - 0.5f,
            Mathf.PerlinNoise(0f, timer * wiggleFrequency) - 0.5f
        ) * wiggleIntensity;

        Vector2 finalDirection = (toPlayer + wiggle).normalized;

        transform.Translate(finalDirection * speed * Time.deltaTime, Space.World);

        float angle = Mathf.Atan2(finalDirection.y, finalDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), rotationSpeed * Time.deltaTime);
    }
}