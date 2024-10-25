using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public Transform player;
    public Animator anim;
    public GameObject sprite;

    private bool isChasing;
    private bool facingRight = true;
    private Rigidbody2D rb;

    public LayerMask groundLayer;
    public Transform groundCheck;
    public float checkDistance = 0.5f; // Distance for checking the platform

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }

        // Launch animation based on speed
        anim.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
    }

    private void ChasePlayer()
    {
        // Check if there is a platform ahead
        bool isGroundAhead = Physics2D.Raycast(groundCheck.position, Vector2.down, checkDistance, groundLayer);

        if (!isGroundAhead)
        {
            // If there is no ground ahead, stop to avoid falling
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }

        Vector3 direction = new Vector3(player.position.x - transform.position.x, 0, 0).normalized;
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

        // Check if we need to flip the character
        if ((direction.x > 0 && !facingRight) || (direction.x < 0 && facingRight))
        {
            Flip();
        }
    }

    private void Patrol()
    {
        // Check if there is ground ahead
        bool isGroundAhead = Physics2D.Raycast(groundCheck.position, Vector2.down, checkDistance, groundLayer);

        if (!isGroundAhead)
        {
            Flip();
        }

        // Move in the current direction
        rb.velocity = new Vector2(facingRight ? speed : -speed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isChasing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isChasing = false;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;

        // Invert character scale on the X-axis
        Vector3 theScale = sprite.transform.localScale;
        theScale.x *= -1;
        sprite.transform.localScale = theScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * checkDistance);
    }
}
