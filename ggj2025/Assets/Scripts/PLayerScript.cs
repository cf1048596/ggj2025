using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class PlayerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float moveSpeed = 8f;
    private float drag = 2f;
    public float buoyancy = 2f;

    private Rigidbody2D rb;
    private Vector2 velocity;

    void Start()
    {
        // get the player's collider
        Collider2D playerCollider = GetComponent<Collider2D>();

        // find all objects tagged "IgnorePlayer"
        GameObject[] boxesToIgnore = GameObject.FindGameObjectsWithTag("ignorePlayCol");

        // Loop through and ignore collisions with each box
        foreach (GameObject box in boxesToIgnore)
        {
            Collider2D boxCollider = box.GetComponent<Collider2D>();

            if (boxCollider != null)
            {
                Physics2D.IgnoreCollision(playerCollider, boxCollider);
            }
        }



        //get the rigidbody2d component
        rb = GetComponent<Rigidbody2D>();
        //disable gravity
        rb.gravityScale = 0f;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //apply movement force
        Vector2 inputForce = new Vector2(horizontal, vertical) * moveSpeed;
        velocity += inputForce * Time.deltaTime;

        //simulate drag
        velocity -= velocity * drag * Time.deltaTime;
        velocity.y += buoyancy * Time.deltaTime;
        velocity = Vector2.ClampMagnitude(velocity, moveSpeed);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ignorePlayCol"))
        {
            return;
        }

        velocity = Vector2.Reflect(velocity, collision.contacts[0].normal) * 0.5f;
    }
}
