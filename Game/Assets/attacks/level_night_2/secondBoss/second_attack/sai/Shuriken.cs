using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (Mathf.Abs(transform.position.x) > 30 || Mathf.Abs(transform.position.y) > 20)
        {
            Destroy(gameObject);
        }
    }
}