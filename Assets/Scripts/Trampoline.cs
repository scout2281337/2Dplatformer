using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float jumpForce = 10f; // The force applied to the object when it hits the jump pad

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that enters the trigger has a Rigidbody2D
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

        if (rb != null)
        {   
            if (rb.velocity.y < -0.1f)
            {
                jumpForce = rb.velocity.y * -2.5f;
                //Debug.Log(jumpForce);
                // Apply an upward force to the object
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }
}
