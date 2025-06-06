using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    public int health = 10;
    private float currentHealth = 0;
    private float damageTaken = 0;
    
    public GameObject bossManager = null;
    
    public GameObject spriteObject;
    public float flashTime = 0.1f;
    public float flashIntensity = 0.1f;
    
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public Collider2D collider;
    public UnityEvent OnEnemyDeath = new UnityEvent();
    public GameObject boosDeathEffect;

    void Start(){
        spriteRenderer = spriteObject.GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        currentHealth = health;
    }

    public float GetHealth(){
        return currentHealth;
    }

    public float GetDamageTaken(){
        return damageTaken;
    }

    public void TakeDamage(float amount){
        if (PerkManager.Instance != null)
        {
            amount *= PerkManager.Instance.damageMultiplier;
        }
        
        currentHealth -= amount;
        damageTaken += amount;
        StartCoroutine(FlashWhite());
        if(currentHealth <= 0){
            damageTaken = health;
            collider.enabled = false;
            animator.SetTrigger("isDead");
            if(boosDeathEffect != null){
                ParticleSystem lightning = boosDeathEffect.transform.GetChild(0).GetComponent<ParticleSystem>();
                ParticleSystem stars = boosDeathEffect.transform.GetChild(1).GetComponent<ParticleSystem>();
                lightning.Play();
                stars.Play();
            }
            FindObjectOfType<PlayerStateManager>().GetComponent<Health>().DisableDamage();
            FindAnyObjectByType<VictoryMenu>().ShowVictory();
        }
    }
    
    private IEnumerator FlashWhite(){
        spriteRenderer.material.SetFloat("_FlashIntensity", flashIntensity);
        yield return new WaitForSeconds(flashTime);
        spriteRenderer.material.SetFloat("_FlashIntensity", 0f);
    }

    public void enemyDeath(){
        //OnEnemyDeath.Invoke();
    }
}
