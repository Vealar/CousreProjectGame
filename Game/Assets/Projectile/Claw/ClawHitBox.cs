using UnityEngine;

public class ClawHitBox : MonoBehaviour
{
    public float damage = 4f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage); 
            if (other.GetComponent<BleedEffect>() == null)
            {
                other.gameObject.AddComponent<BleedEffect>(); 
            }
        }
    }
}