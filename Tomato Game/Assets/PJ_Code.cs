using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class movment : MonoBehaviour
{
    public float moveSpeed;
    [Header("Jump System")]
    [SerializeField] private float jumpTime;
    [SerializeField] private float jumpForce;
    [SerializeField] private float fallMultiplaier;
    [SerializeField] private float jumpMultiplaier;
    [SerializeField] private TrailRenderer tr;

    private Rigidbody2D rb;

    public Transform groundCheck;

    public LayerMask whatIsGround;

    Vector2 vecGravity;

    private bool isFacingRight = true;

    //private bool isJumping;
    private bool isGrounded;

    private float horizontal;
    private bool canDash = true;
    private bool isDashing;
    public float dashForce;
    public float dashingTime;
    public float dashingCooldown;

    public Animator anim;
    public SpriteRenderer PlayerSR;


    void Start()
    {
        vecGravity = new Vector2 (0, -Physics2D.gravity.y);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDashing)
        {
            anim.SetBool("isDashing", canDash);
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal > .1f || horizontal < -.1f )
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        Flip();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
            anim.SetBool("isJumping", !isGrounded);
        }

        if (rb.velocity.y <0)
        {
            rb.velocity -= vecGravity * fallMultiplaier * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            anim.SetBool("isDashing", canDash);
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        anim.SetFloat("xVelocity", Math.Abs(rb.velocity.x));
        anim.SetFloat("yVelocity", rb.velocity.y);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal<0f || !isFacingRight && horizontal>0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
        anim.SetBool("isJumping", !isGrounded);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashForce, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;

    }
}
