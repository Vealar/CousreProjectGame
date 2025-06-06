using System.Collections;
using UnityEngine;

public class BossSphere : MonoBehaviour
{
    public Animator animator;
    public float homingSpeed = 22f;
    public float destroyAfterLaunch = 5f;

    private Rigidbody2D rb;
    private Enemy enemyDamage;
    private Transform player;
    private bool hasLaunched = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyDamage = GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (animator == null)
            animator = GetComponent<Animator>();

        if (enemyDamage != null)
            enemyDamage.enabled = false;

        StartCoroutine(PhaseSequence());
    }

    private IEnumerator PhaseSequence()
    {
        yield return new WaitForSeconds(2f);
        animator.SetTrigger("StartPhase1");

        yield return new WaitForSeconds(2f);
        animator.SetTrigger("StartPhase2");

        yield return new WaitForSeconds(2f);
        animator.SetTrigger("StartPhase3");

        yield return new WaitForSeconds(2f);
        animator.SetTrigger("StartPhase4");

    }

    public void LaunchAtPlayer()
    {
        if (hasLaunched) return;
        hasLaunched = true;

        if (enemyDamage != null)
            enemyDamage.enabled = true;

        Destroy(gameObject, destroyAfterLaunch);
    }

    void Update()
    {
        if (!hasLaunched || player == null) return;
        
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * homingSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (hasLaunched)
            {
                StartCoroutine(DestroyAfterDelay(0.05f));
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
