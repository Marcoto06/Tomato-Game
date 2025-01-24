using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movment : MonoBehaviour
{
    public float moveSpeed;
    [Header("Jump System")]
    [SerializeField] float jumpTime;
    [SerializeField] float jumpForce;
    [SerializeField] float fallMultiplaier;
    [SerializeField] float jumpMultiplaier;

    Rigidbody2D rb;

    public Transform groundCheck;

    public LayerMask whatIsGround;

    Vector2 vecGravity;

    bool isJumping;
    float jumpCounter;

    public Animator anim;
    public SpriteRenderer PlayerSR;

    
    // Start is called before the first frame update
    void Start()
    {
        vecGravity = new Vector2 (0, -Physics2D.gravity.y);
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rb.velocity.y);

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

        if (rb.velocity.y <0)
        {
            rb.velocity -= vecGravity * fallMultiplaier * Time.deltaTime;
        }
    }

    bool isGrounded()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1.09f, 0.2f), CapsuleDirection2D.Horizontal, 0, whatIsGround);
    }
}
