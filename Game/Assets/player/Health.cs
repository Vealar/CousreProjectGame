using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour{
    public Collider2D collider2D;
    private Animator animator;

    public int health = 3;
    public float invulnerabilityTime = 6f;

    public UnityEvent<int> OnPlayerHealthChange = new UnityEvent<int>();
    private bool canBeDamaged = true;

    public void DisableDamage()
    {
        canBeDamaged = false;
    }

    public int getHealth(){
        return health;
    }

    private void Start() {
        animator = GetComponent<Animator>();
        if (PerkManager.Instance != null)
        {
            health += PerkManager.Instance.extraHp;
        }
        OnPlayerHealthChange.Invoke(health);
    }

    public void takeDamage(){
        if (!canBeDamaged) return;
        
        health -= 1;
        OnPlayerHealthChange.Invoke(health);
        Debug.Log(health);

        // animator.SetTrigger("hit");
        if (health <= 0){
            if (PerkManager.Instance.canRevive)
            {
                health = 1;
                PerkManager.Instance.canRevive = false;
                OnPlayerHealthChange.Invoke(health);
                return;
            }
            collider2D.enabled = false;
            animator.SetTrigger("death");
            GameStatsManager.AddDeath();
            PlayerPrefs.Save();
            FindAnyObjectByType<GameOverMenu>().ShowGameOver();

        } else {
            Invulnerability();
        }
    }
    

    public void finishHit(){
        // animator.ResetTrigger("hit");
    }

    void Invulnerability(){
        StartCoroutine(BlinkSprite());
        StartCoroutine(StartInvulnerability());
    }

    IEnumerator StartInvulnerability(){
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        yield return new WaitForSeconds(invulnerabilityTime);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
    }

    IEnumerator BlinkSprite(){
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        for (int i = 0; i < 6; i++){
            spriteRenderer.color = new Color(1f,1f,1f,0.5f);
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = new Color(1f,1f,1f,1f);
            yield return new WaitForSeconds(0.1f);
        }
    }

}
