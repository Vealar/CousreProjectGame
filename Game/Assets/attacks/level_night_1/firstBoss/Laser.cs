using UnityEngine;

public class Laser : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D damageCollider;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        damageCollider = GetComponent<BoxCollider2D>();
        spriteRenderer.enabled = false;
        damageCollider.enabled = false;
    }

    public void EnableVisual()
    {

        spriteRenderer.enabled = true;
    }

    public void EnableDamage()
    {
        

        damageCollider.enabled = true;
    }

    public void DisableDamage()
    {

        damageCollider.enabled = false;
    }

    public void DestroyLaser()
    {

        Destroy(gameObject);
    }
}