using UnityEngine;

public class GhostClawProjectile : MonoBehaviour
{
    public float damage = 1.5f;
    
    private Rigidbody2D rigidbody2D;
    private Collider2D collider2D;
    private Animator animator;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

        if (rigidbody2D.linearVelocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(rigidbody2D.linearVelocity.y, rigidbody2D.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        EnemyHealth enemy = hitInfo.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            rigidbody2D.linearVelocity = Vector2.zero;
            collider2D.enabled = false;
            animator.SetTrigger("hit");
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}