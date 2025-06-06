using UnityEngine;

using UnityEngine;

public class bombProjectile : MonoBehaviour
{
    public GameObject shurikenPrefab;
    public float shurikenSpeed = 8f;
    private Rigidbody2D rb;
    private Animator animator;
    public float flightTime = 1f; 

    void Awake()
    {
        animator = GetComponent<Animator>();
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
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Boss"))
        {
            animator.SetTrigger("expl");
        }
    }

    void Explode()
    {
        int shurikenCount = 8;
        float angleStep = 360f / shurikenCount;

        for (int i = 0; i < shurikenCount; i++)
        {
            float angle = i * angleStep;
            float rad = angle * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;

            GameObject shuriken = Instantiate(shurikenPrefab, transform.position, Quaternion.identity);
        
            float angleDeg = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            shuriken.transform.rotation = Quaternion.Euler(0, 0, angleDeg);

            Rigidbody2D rb = shuriken.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.linearVelocity = dir * shurikenSpeed;
        }

        Destroy(gameObject);
    }
    
    

    void Update()
    {
        if (Mathf.Abs(transform.position.x) > 30 || Mathf.Abs(transform.position.y) > 20)
        {
            Destroy(gameObject);
        }
    }
}

