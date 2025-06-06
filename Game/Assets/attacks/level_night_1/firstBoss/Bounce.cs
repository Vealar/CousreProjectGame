using UnityEngine;

public class Bounce : MonoBehaviour
{
    public float flightTime = 1f; 
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void LaunchTowards(Vector2 target)
    {
        Vector2 start = transform.position;

        Vector2 toTarget = target - start;

        Vector2 velocity = new Vector2(
            toTarget.x / flightTime,
            (toTarget.y + 0.5f * Mathf.Abs(Physics2D.gravity.y) * flightTime * flightTime) / flightTime
        );
        velocity.y += 1;
        rb.linearVelocity = velocity;
        rb.gravityScale = 1f; 
    }

    void Update()
    {
        if (Mathf.Abs(transform.position.x) > 30 || Mathf.Abs(transform.position.y) > 20)
        {
            Destroy(gameObject);
        }
    }
}