using UnityEngine;

public class enemyScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Transform player;
    public float detectionRange = 10f;
    public float moveSpeed = 2f;
    private float hp = 100f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Check if the player is within detection range
        if (distanceToPlayer <= detectionRange)
        {
            // Move towards the player
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("bullet"))
        {
            hp -= 10f;
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
            Destroy(collider.gameObject);
        }
        if (collider.CompareTag("Player"))
        {
            PlayerScript playerScript = collider.gameObject.GetComponent<PlayerScript>();
            if (playerScript != null)
            {
                playerScript.hp -= 10; // Decrease HP by 10 (or whatever amount you want)
            }
        }

    }
}
