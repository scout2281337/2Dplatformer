using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement2 : MonoBehaviour
{
    [Header("DeBug")]
    public Vector2 decelerationVector;
    public Vector2 speed;

    [Header("Run")]
    public float acceleration = 40f; 
    public float runDeceleration = 0.9f;
    public float maxSpeed = 12f;
    public Vector2 moveVector;

    [Header("Jump")]
    public float jumpStartForce = 7;
    public float jumpForce = 20;
    public float jumpTime = 0.2f;
    public int maxJumps = 2;
    public float jumpDeceleration = 0.3f;
    private int jumpAmount = 0;
    private float lastTimeJumped = 0;
    private bool isJumping = false;
    private bool canJump = true;

    [Header("Dash")]
    public float dashForce = 10;
    public float dashCooldown = 1;
    public float dashLowerGravityTime = 0.3f;
    public Vector2 dashDeceleration = new Vector2(0.5f, 0.5f);
    private float lastTimeDashed;

    [Header("RigidBody")]
    private Rigidbody2D rb;

    [Header("Animator")]
    public Animator animator;
    private bool facingRight = true; // ���������, ������� �� �������� �����

    [Header("GroundCheck")]
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
        //debug
        speed = rb.velocity;

        Run();
        CheckGround();
        GravityChange();
    }
    void Update()
    {
        moveVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // ����������� ���������

        // ���������� ��������
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x)); // ���������� Abs ��� �������� ���������� �� �����������
        animator.SetFloat("yVelocity", rb.velocity.y);

        // ������������ ���������
        if (moveVector.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveVector.x < 0 && facingRight)
        {
            Flip();
        }

        //Jump
        if (Input.GetKeyDown("space"))
        {
            JumpStart();
        }

        if (Input.GetKey("space"))
        {
            Jump();
        }

        if (Input.GetKeyUp("space"))
        {
            JumpEnd();
        }

        //Dash
        if (Input.GetKeyDown("left shift"))
        {
            Dash();
        }
    }
    private void JumpStart()
    {
        if (canJump)
        {
            lastTimeJumped = Time.time;
            isJumping = true;

            animator.SetTrigger("jump");

            if (rb.velocity.y < 0)
            {
                Decelerate(1, jumpDeceleration);
            }

            rb.AddForce(new Vector2(0, jumpStartForce), ForceMode2D.Impulse);
        }
    }

    private void Jump()
    {
        if (canJump && lastTimeJumped + jumpTime > Time.time)
        {
            rb.AddForce(new Vector2(0, jumpForce * Time.deltaTime * 100), ForceMode2D.Force); // ��������� ������������ �������� ��� ������
        }
        else
        {
            isJumping = false;
        }
    }

    private void JumpEnd()
    {
        if (canJump)
        {
            jumpAmount++;
            isJumping = false;

            if (jumpAmount >= maxJumps)
            {
                canJump = false; // ��������� ����������� �������, ���� ��������� ������
            }
        }
    }

    private void Dash()
    {
        if (lastTimeDashed + dashCooldown < Time.time)
        {
            DecelerateByVector(dashDeceleration.x, dashDeceleration.y, moveVector);
            rb.AddForce(moveVector.normalized * new Vector2(dashForce, dashForce), ForceMode2D.Impulse);
            lastTimeDashed = Time.time;
        }

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
        //Debug.Log(canJump);
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
            Decelerate(runDeceleration, 1);
        }

    }

    private void DecelerateByVector(float xDeceleration, float yDeceleration, Vector2 actionDir)
    {
        float dirCoefficient = (Vector2.Dot(rb.velocity.normalized, actionDir.normalized) + 1) / 2;

        Decelerate(Mathf.Lerp(xDeceleration, 1, dirCoefficient), Mathf.Lerp(yDeceleration, 1, dirCoefficient));

        decelerationVector = new Vector2(Mathf.Lerp(xDeceleration, 1, dirCoefficient), Mathf.Lerp(yDeceleration, 1, dirCoefficient));
    }

    private void Decelerate(float xDeceleration, float yDeceleration)
    {
        // multiplying rb.velocity to slow down the player
        if (rb.velocity.magnitude > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * xDeceleration, rb.velocity.y * yDeceleration);

            // Stop the rigidbody if the deceleration is very small to avoid sliding
            if (rb.velocity.magnitude < 0.1f)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    private void GravityChange()
    {
        // for jump
        if (lastTimeDashed + dashLowerGravityTime > Time.time)
        {
            rb.gravityScale = 0;
        }
        else if (isJumping)
        {
            rb.gravityScale = 1;
        }
        else
        {
            rb.gravityScale = 2;
        }
    }
}