using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BossController : MonoBehaviour
{
    public Animator animator;
    public GameObject bounce;
    public Transform firePoint;
    public float projectileSpeed = 10f;

    public float attackInterval = 5f;

    private bool isAlive = true;
    public GameObject laserPrefab;
    public Transform laserSpawnPoint;

    public void FireLaser()
    {
        animator.SetTrigger("att_lower");
    }

    public void ActionLaser()
    {
        Instantiate(laserPrefab, laserSpawnPoint.position, Quaternion.identity);
    }


    public void FireTripleShot()
    {
        animator.SetTrigger("att_upper");
    }

    public void ActionTripleShot()
    {
        StartCoroutine(FireTripleShotCoroutine());
    }

IEnumerator FireTripleShotCoroutine()
    {
        Vector2 startPos = firePoint.position;

        for (int i = 0; i < 5; i++)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                Vector2 playerPos = player.transform.position;

                GameObject proj = Instantiate(bounce, startPos, Quaternion.identity);
                Bounce bounceScript = proj.GetComponent<Bounce>();

                if (bounceScript != null)
                    bounceScript.LaunchTowards(playerPos);
            }

            yield return new WaitForSeconds(0.6f);
        }
    }




    public void Die()
    {
        isAlive = false;
    }
}