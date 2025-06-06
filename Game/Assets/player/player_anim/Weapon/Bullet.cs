// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class Bullet : MonoBehaviour
// {
//     public int damage = 1;
//     
//     private Rigidbody2D rigidbody2D;
//     private Collider2D collider2D;
//     private Animator animator;
//
//     void Start()
//     {
//         rigidbody2D = GetComponent<Rigidbody2D>();
//         collider2D = GetComponent<Collider2D>();
//         animator = GetComponent<Animator>();
//     }
//
//     void OnTriggerEnter2D(Collider2D hitInfo)
//     {
//         EnemyHealth enemy = hitInfo.GetComponent<EnemyHealth>();
//         if (enemy != null)
//         {
//             enemy.TakeDamage(damage);
//             rigidbody2D.linearVelocity = Vector2.zero;
//             collider2D.enabled = false;
//             animator.SetTrigger("hit");
//         }
//     }
//
//     void DestroyBullet()
//     {
//         Destroy(gameObject);
//     }
// }