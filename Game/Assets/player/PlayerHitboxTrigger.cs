using UnityEngine;

public class PlayerHitboxTrigger : MonoBehaviour
{
    public Health health;

    void OnTriggerEnter2D(Collider2D other) {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null && enemy.enabled && health.getHealth() > 0){
            health.takeDamage();
        }
    }
}