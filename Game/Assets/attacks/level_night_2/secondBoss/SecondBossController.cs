using System.Collections;
using UnityEngine;

public class SecondBossController : MonoBehaviour
{
    [Header("References")] public Transform player;
    public Animator animator;
    public GameObject slashEffectPrefab;
    public BoxCollider2D boxCollider;
    public Enemy enemy;
    private Vector3 startPosition;
    
    [Header("Flying Head")] public GameObject flyingHeadPrefab;
    public Transform headSpawnPoint;
    public float flyingHeadDuration = 5f;

    [Header("Slash Attack")] public float attackRadius = 2f;
    public float teleportDelay = 1.3f;
    public float attackDelay = 0.5f;
    public LayerMask playerLayer;
    
    [Header("Spear Attack")]
    public Transform[] spearThrowPoints;
    public Transform[] bossPositions;
    public GameObject spearPrefab;
    public float spearSpeed = 10f;
    private bool isAttacking = false;

    void Start()
    {
        startPosition = transform.position;
        boxCollider = GetComponentInChildren<BoxCollider2D>();
        enemy = GetComponentInChildren<Enemy>();
    }

    public IEnumerator DoFirstAttack()
    {
        yield return new WaitForSeconds(4f);
        if (isAttacking) yield break;
        isAttacking = true;

        animator.SetTrigger("delete");

        GameObject head = Instantiate(flyingHeadPrefab, headSpawnPoint.position, Quaternion.identity);

        for (int i = 0; i < 4; i++)
        {
            TeleportAndSlash();
            yield return new WaitForSeconds(3f);
        }
        Destroy(head);
        transform.position = startPosition;
        animator.SetTrigger("idle");
        isAttacking = false;
        yield return new WaitForSeconds(1f);
    }

    private void TeleportAndSlash()
    {
        enemy.enabled = false;
        boxCollider.enabled = false;
        animator.SetTrigger("teleport_1");
    }

    public void Teleport()
    {
        Vector2 offset = new Vector2(1.5f, 0f);
        transform.position = player.position + (Vector3)offset;
        animator.SetTrigger("teleport_2");
    }

    public void DamageDeal()
    {
        enemy.enabled = true;
        boxCollider.enabled = true;
    }

    public void Attack()
    {
        animator.SetTrigger("slash");
    }

    public GameObject bombPrefab;
    public Transform bombSpawnPoint;
    public IEnumerator DoSecondAttack(){
        if (isAttacking) yield break;
        isAttacking = true;
        StartCoroutine(ThrowBombsAtPlayer());
        yield return new WaitForSeconds(0.1f);
    }
    public IEnumerator ThrowBombsAtPlayer()
    {
        

        for (int i = 0; i < 7; i++)
        {
            animator.SetTrigger("bomb");
            yield return new WaitForSeconds(3f);
        }
        isAttacking = false;
        yield return new WaitForSeconds(1f);
    }

    public void bombAction()
    {
        Vector2 startPos = bombSpawnPoint.position;
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Vector2 playerPos = player.transform.position;

            GameObject proj = Instantiate(bombPrefab, startPos, Quaternion.identity);
            bombProjectile bounceScript = proj.GetComponent<bombProjectile>();

            if (bounceScript != null)
                bounceScript.LaunchTowards(playerPos);
        }
    }
    
    public int spearThrowCount = 7;       
    public float betweenThrowsDelay = 1.8f;

    public IEnumerator DoThirdAttack()
    {
        if (isAttacking) yield break;
        isAttacking = true;

        for (int i = 0; i < spearThrowCount; i++)
        {
            int index = Random.Range(0, bossPositions.Length);

            transform.position = bossPositions[index].position;

            animator.SetTrigger("throw");
            yield return new WaitForSeconds(0.4f);

            ThrowSpear(spearThrowPoints[index]);

            yield return new WaitForSeconds(betweenThrowsDelay);
        }

        transform.position = startPosition;
        animator.SetTrigger("idle");
        isAttacking = false;
        yield return new WaitForSeconds(1f);
    }


    private void ThrowSpear(Transform throwPoint)
    {
        GameObject spear = Instantiate(spearPrefab, throwPoint.position, Quaternion.identity);

        Animator spearAnim = spear.GetComponent<Animator>();
        Rigidbody2D rb = spear.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Vector2 direction = Vector2.left;
            rb.gravityScale = 0f;
            rb.linearVelocity = direction * spearSpeed * 2.2f;

            spear.transform.right = direction; 
        }
    }

}
