using UnityEngine;

public class FanProjectile : MonoBehaviour
{
    public float damage = 0.8f;
    public float speed = 10f;

    private Vector2 startPoint;
    private Vector2 controlPoint;
    private Vector2 endPoint;

    private float duration;
    private float progress = 0f;
    private bool moving = false;

    private Collider2D collider2D;
    private Animator animator;

    public void SetTrajectory(Vector2 start, Vector2 end, float arcHeight)
    {
        startPoint = start;
        endPoint = end;

        Vector2 mid = (start + end) / 2f;
        Vector2 arcOffset = Vector2.Perpendicular((end - start).normalized) * arcHeight;
        controlPoint = mid + arcOffset;

        float distance = Vector2.Distance(start, end);
        duration = distance / speed;

        moving = true;
    }

    void Start()
    {
        collider2D = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!moving) return;

        progress += Time.deltaTime / duration;
        float t = Mathf.Clamp01(progress);

        Vector2 pos = Mathf.Pow(1 - t, 2) * startPoint +
                      2 * (1 - t) * t * controlPoint +
                      Mathf.Pow(t, 2) * endPoint;

        transform.position = pos;

        if (t < 1f)
        {
            float nextT = Mathf.Clamp01(t + 0.01f);
            Vector2 nextPos = Mathf.Pow(1 - nextT, 2) * startPoint +
                              2 * (1 - nextT) * nextT * controlPoint +
                              Mathf.Pow(nextT, 2) * endPoint;

            Vector2 dir = (nextPos - pos).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        if (t >= 1f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (!moving) return;

        EnemyHealth enemy = hitInfo.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            moving = false;
            collider2D.enabled = false;
            animator.SetTrigger("hit");
        }

        if (hitInfo.CompareTag("OneWayPlatform"))
        {
            moving = false;
            collider2D.enabled = false;
            animator.SetTrigger("hit");
        }
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
