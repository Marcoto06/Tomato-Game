using System.Collections;
using System.Collections.Generic;
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

    private bool isJumping;
    private float jumpCounter;

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

    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        Flip();

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
            jumpCounter = 0;
        }

        if (rb.velocity.y > 0 && isJumping)
        {
            jumpCounter = Time.deltaTime;
            if (jumpCounter > jumpTime) isJumping = false;
            rb.velocity += vecGravity * jumpMultiplaier * Time.deltaTime;
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
        if (Input.GetButtonDown("Jump"))
        {
            isJumping = false;
        }

        if (rb.velocity.y <0)
        {
            rb.velocity -= vecGravity * fallMultiplaier * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
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

    bool isGrounded()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1.09f, 0.2f), CapsuleDirection2D.Horizontal, 0, whatIsGround);
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
