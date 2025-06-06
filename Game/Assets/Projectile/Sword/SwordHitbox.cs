using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    public float damage = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }
}