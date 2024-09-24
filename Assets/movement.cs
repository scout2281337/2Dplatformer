using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    float xInput;
    public float speed;
    public float jumpForce;
    
    private Rigidbody2D rb;
    private Vector2 vec2;
    private bool canJump;
    
    
    public GameObject groundCheck;
    public LayerMask groundMask;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        vec2 = new Vector2(xInput * speed, rb.velocity.y);
        rb.velocity = vec2;



        if (Input.GetKeyDown("space") && canJump)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // Применение силы для прыжка
        }

        CheckGround();

    }

    private void CheckGround() 
    {

        Collider2D groundCollision = Physics2D.OverlapCircle(groundCheck.transform.position, 0.1f, groundMask);
    
        if ( groundCollision != null ) { canJump = true; }
        else { canJump = false; }
    }
    


}
