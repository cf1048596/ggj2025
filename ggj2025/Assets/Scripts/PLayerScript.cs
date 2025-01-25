using System.Runtime.CompilerServices;
using TMPro;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.Controls;

public class PlayerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float moveSpeed = 8f;
    private float drag = 2f;
    public float buoyancy = 2f;
    private float hp = 50f;

    private Rigidbody2D rb;
    private Vector2 velocity;

    void Start()
    {
        //get the player's collider
        Collider2D playerCollider = GetComponent<Collider2D>();


        //get the rigidbody2d component
        rb = GetComponent<Rigidbody2D>();
        //disable gravity
        rb.gravityScale = 0f;

        GameObject[] ignoreObjects = GameObject.FindGameObjectsWithTag("ignorePlayCol");

        //loop through each object and disable its collisions with this object
        foreach (var obj in ignoreObjects)
        {
            Collider2D otherCollider = obj.GetComponent<Collider2D>();

            if (otherCollider != null)
            {
                //ignore the collision between this object's collider and the other object's collider
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), otherCollider, true);
            }
        }
    }

    void Update()
    {
        float rotationSpeed = 5f;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //apply movement force
        Vector2 inputForce = new Vector2(horizontal, vertical) * moveSpeed;
        velocity += inputForce * Time.deltaTime;

        //simulate drag
        velocity -= velocity * drag * Time.deltaTime;
        velocity.y += buoyancy * Time.deltaTime;
        velocity = Vector2.ClampMagnitude(velocity, moveSpeed);
        //track the current mouse position in world space
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; //keep the z value as 0 for 2D games

        //set the target position as the mouse position
        Vector3 targetPosition = mousePos;

        //calculate the direction from the character to the mouse
        Vector3 direction = (targetPosition - transform.position).normalized;

        //calculate the angle we need to rotate to
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //interpolate the rotation with a delay
        float smoothAngle = Mathf.LerpAngle(transform.eulerAngles.z, angle, Time.deltaTime * rotationSpeed);

        //apply the smoothed rotation
        transform.rotation = Quaternion.Euler(0, 0, smoothAngle);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("enemyfish"))
        {
            hp -= 10;
        }
    }
}
