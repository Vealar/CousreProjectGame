using System.Collections;
using UnityEngine;

public class ShadowMover : MonoBehaviour
{
    public float speed = 4f;
    private Vector3[] path;
    private int currentIndex = 0;
    private bool canMove = false;

    public void InitPath(Vector3[] givenPath)
    {
        path = givenPath;

        Vector2 direction = path[1] - path[0];
        if (direction.x < 0)
            transform.localScale = new Vector3(-0.5f, 0.5f, 1f); 
        else
            transform.localScale = new Vector3(0.5f, 0.5f, 1f);  

        StartCoroutine(WaitThenMove());
    }

    private IEnumerator WaitThenMove()
    {
        yield return new WaitForSeconds(1.2f); 
        canMove = true;
        GetComponent<Animator>().SetTrigger("Fly"); 
    }

    void Update()
    {
        if (!canMove || path == null || currentIndex >= path.Length) return;

        transform.position = Vector3.MoveTowards(transform.position, path[currentIndex], speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, path[currentIndex]) < 0.1f)
            currentIndex++;

        if (currentIndex >= path.Length)
            Destroy(gameObject);
    }
}


