using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement2 : MonoBehaviour
{
    private Vector2 moveVector;
    public float acceleration;
    public float deceleration;
    public float maxSpeed;
    public float currentspeed;

    public float jumpForce;
    public int maxJumps = 2; // �������� ������� ��� �������� ������
    private int jumpAmount = 0;

    public float dashForce;

    private Rigidbody2D rb;
    private bool canJump;

    public Animator anim;
    private bool facingRight = true; // ���������, ������� �� �������� �����

    public GameObject character;
    public GameObject groundCheck;
    public LayerMask groundMask;
    public float groundCheckRadius = 0.1f; // ������ �������� ������� �����

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Run();
        CheckGround();
        currentspeed = rb.velocity.magnitude;
    }
    void Update()
    {
        moveVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // ����������� ���������

        // ���������� ��������
        anim.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x)); // ���������� Abs ��� �������� ���������� �� �����������
        anim.SetFloat("yVelocity", rb.velocity.y);

        // ������������ ���������
        if (moveVector.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveVector.x < 0 && facingRight)
        {
            Flip();
        }

        // ������
        if (Input.GetKeyDown("space") && canJump)
        {
            Jump();
        }


        if (Input.GetKeyDown("left shift"))
        {
            Dash();
        }

    }

    private void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse); // ��������� ������������ �������� ��� ������
        jumpAmount++;

        anim.SetBool("isJumping", true); // �������� ������
        //Debug.Log(canJump);

        if (jumpAmount >= maxJumps)
        {
            canJump = false; // ��������� ����������� �������, ���� ��������� ������
        }
    }
    private void Dash()
    {
        //Debug.Log("Dash");
        rb.AddForce(moveVector.normalized * new Vector2(dashForce, 1.5f * dashForce), ForceMode2D.Impulse);
    }


    private void CheckGround()
    {
        // ��������� ������� � ������ � ������� OverlapCircle
        Collider2D groundCollision = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRadius, groundMask);

        if (groundCollision != null) // ���� �������� �� �����
        {
            canJump = true;
            jumpAmount = 0; // ���������� ���������� �������

        }

        anim.SetBool("isJumping", !canJump); // ����� �� �����, ������ ��������
        Debug.Log(canJump);
    }

    private void Flip()
    {
        // ������ ����������� �������� ���������
        facingRight = !facingRight;

        // ����������� ��������� ����� ��������� �� ��� X
        Vector3 theScale = character.transform.localScale;
        theScale.x *= -1; // �������������� �� ��� X
        character.transform.localScale = theScale;
    }

   
    private void Run()
    {
        if (moveVector.x != 0 && Mathf.Abs(rb.velocity.x) < maxSpeed)
        {
            float desiredSpeed = moveVector.x * maxSpeed; // Speed the player wants to reach
            float speedDif = Mathf.Abs((desiredSpeed - rb.velocity.x) / maxSpeed); // Difference between current speed and desired speed
            rb.AddForce(new Vector2(speedDif * acceleration * moveVector.x, 0)); // Apply force in the direction of movement
        }
        
        else if (moveVector.x == 0)
        {
            Decelerate();
        }
        
    }


    private void Decelerate()
    {
        // Apply force in the opposite direction to current velocity to slow down
        if (rb.velocity.magnitude > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * deceleration, rb.velocity.y);

            // Stop the rigidbody if the deceleration is very small to avoid sliding
            if (rb.velocity.magnitude < 0.1f)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }
}
