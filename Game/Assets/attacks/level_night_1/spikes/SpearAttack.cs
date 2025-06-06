using UnityEngine;

public class SpearAttack : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D coll;

    void Start()
    {
        animator = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();

        if (coll != null)
        {
            coll.enabled = false;
        }
    }

    public void StartAttackSequence()
    {
        animator.SetTrigger("blink");
    }

    public void Stab()
    {
        animator.SetTrigger("action");
    }

    public void EnableDamage()
    {
        if (coll != null)
            coll.enabled = true;
    }

    public void DisableDamage()
    {
        if (coll != null)
            coll.enabled = false;
    }
}