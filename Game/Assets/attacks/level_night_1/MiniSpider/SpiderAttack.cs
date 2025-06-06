using UnityEngine;

public class SpiderAttack : MonoBehaviour
{
    public float speed = 3f;
    public float lifetime = 5f;
    public int damage = 1;
    private bool hasHit = false;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
    }

    void FixedUpdate()
    {
        float moveDelta = speed * Time.fixedDeltaTime;
        Vector2 newPosition = rb.position + Vector2.left * moveDelta;
        rb.MovePosition(newPosition);
    }
    
}
