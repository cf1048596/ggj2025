using UnityEngine;

public class bulletScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform firePoint;
    public float bulletSpeed = 20f;

    private Collider2D bulletCollider;

    void Start()
    {
        bulletCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("ignorePlayCol"))
        {
            // Ignore collision with this object
            Collider2D otherCollider = collision.collider;
            Physics2D.IgnoreCollision(bulletCollider, otherCollider, true);
        }
    }
}
