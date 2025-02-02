using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;
using JetBrains.Annotations;
using UnityEngine.UI;

public class movment : MonoBehaviour
{
    public float maxSpeed;
    [Header("Jump System")]
    [SerializeField] public float jumpTime;
    [SerializeField] public float jumpForce;
    [SerializeField] public float fallMultiplaier;
    [SerializeField] public float jumpMultiplaier;

    public Rigidbody2D rb;

    public Transform groundCheck;

    public LayerMask[] whatIsGround;

    Vector2 vecGravity;

    private bool isFacingRight = true;

    //private bool isJumping;
    private bool isGrounded;

    private float horizontal;
    private bool canDash = true;
    private bool isDashing;
    public float dashForce;
    public float dashEnd;
    public float dashingTime;
    public float dashingCooldown;
    public bool isAttacking;

    public Animator anim;
    public SpriteRenderer PlayerSR;

    public GameObject attackPoint;
    public GameObject manager;
    public GameObject brancaGTomato;
    public GameObject brancaNTomato;
    public GameObject dard;
    public GameObject N_dard;
    public GameObject gameOver;
    public GameObject mageUI;
    public Image MagCurrent;
    public Image MagAlt;
    public Sprite bastoMagSprite;
    public Sprite orbeMagSprite;
    public float radius;
    public LayerMask enemies;
    public float PJ_DAM;

    public int MAX_HP;
    public int Current_HP;
    public int knockBack;
    public string current_class;
    public string C_atk = "melee";
    public bool hit;
    public int knockBackRotate;
    public bool isDamaged;
    public float runAccelAmount;
    public float runDeccelAmount;

    public GameObject[] hearts;

    void Start()
    {
        mageUI.SetActive(false);
        isDamaged = false;
        gameOver = GameObject.FindGameObjectWithTag("gameOver");
        gameOver.SetActive(false);
        manager = GameObject.FindGameObjectWithTag("manager");
        Time.timeScale = 1;
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        string path = Application.persistentDataPath + "/saveData.json";
        if (File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            saveData loadedData = JsonUtility.FromJson<saveData>(json);
            current_class = loadedData.player_class;
            Current_HP = loadedData.current_HP;
        }
        if (current_class == "melee")
        {
            anim.SetBool("isMelee", true);
            anim.SetBool("isDistance", false);
            anim.SetBool("IsMage", false);
        }
        else if (current_class == "ranged")
        {
            anim.SetBool("isMelee", false);
            anim.SetBool("isDistance", true);
            anim.SetBool("IsMage", false);
        }
        else if (current_class == "mage")
        {
            mageUI.SetActive(true);
            anim.SetBool("IsMage", true);
            anim.SetBool("isDistance", false);
            anim.SetBool("isMelee", false);
            C_atk = "melee";
        }
        PJ_DAM = 50; //linia temporal. treure mes tard
    }

    void Update()
    {
        for (int i = 0; i < hearts.Length; i++) 
        { 
            if (i < Current_HP)
            {
                hearts[i].SetActive(true);
            } else { hearts[i].SetActive(false); }
        }
        if (C_atk == "melee")
        {
            MagCurrent.sprite = bastoMagSprite;
            MagAlt.sprite = orbeMagSprite;
        }
        else
        {
            MagCurrent.sprite = orbeMagSprite;
            MagAlt.sprite = bastoMagSprite;
        }
        if (isDashing)
        {
            anim.SetBool("isDashing", canDash);
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal > .1f || horizontal < -.1f)
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

        if (rb.velocity.y < 0)
        {
            rb.velocity -= vecGravity * fallMultiplaier * Time.deltaTime;
        }

        if (Input.GetButtonDown("R1") && canDash)
        {
            anim.SetBool("isDashing", canDash);
            StartCoroutine(Dash());
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (isAttacking == false)
            {
                StartCoroutine(AttAn());
            }
        }
        if (Input.GetButtonDown("L1"))
        {
            if (C_atk == "melee")
            {
                C_atk = "ranged";
                anim.SetBool("isRanged", true);
            }
            else
            {
                C_atk = "melee";
                anim.SetBool("isRanged", false);
            }
        }
        if (hit == true && isDamaged == false)
        {
            StartCoroutine(Hit());
        }
        if(Current_HP <= 0)
        {
            StartCoroutine(Die());
        }


    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        if (hit == false)
        {
            Run(1);
            //rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
            anim.SetFloat("xVelocity", Math.Abs(rb.velocity.x));
            anim.SetFloat("yVelocity", rb.velocity.y);
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ground") | collision.CompareTag("wall"))
        {
            isGrounded = true;
            anim.SetBool("isJumping", !isGrounded);
        }
        if (collision.CompareTag("powerUp_H"))
        {
            MAX_HP++;  
            Current_HP = MAX_HP;  
            Destroy(collision.gameObject); 
        }
        if (collision.CompareTag("powerUp_D"))
        {
            PJ_DAM = PJ_DAM + 25;
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashForce, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        rb.velocity = new Vector2(transform.localScale.x * dashEnd, rb.velocity.y);
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    private IEnumerator AttAn()
    {
        isAttacking = true;
        anim.SetBool("isAttacking", isAttacking);
        if (current_class != "ranged")
        {
            yield return new WaitForSeconds(0.2666666672f);
        }
        else
        {
            yield return new WaitForSeconds(0.4166666675f);
        }
        brancaGTomato.SetActive(false);
        brancaNTomato.SetActive(false);
        anim.SetBool("isAttacking", false);
        isAttacking = false;
    }

    public void Attack()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemies);

        foreach (Collider2D enemyGameobject in enemy)
        {
            Debug.Log("Hit enemy");
            enemyGameobject.GetComponentInParent<enemyScript>().EN_CHP -= PJ_DAM;
            enemyGameobject.GetComponentInParent<enemyScript>().hit = true;
            enemyGameobject.GetComponentInParent<enemyScript>().knockBackRotate = gameObject.transform.localScale.x;
            enemyGameobject.GetComponentInParent<BOSS_Script>().EN_CHP -= PJ_DAM;
            enemyGameobject.GetComponentInParent<BOSS_Script>().hit = true;
            enemyGameobject.GetComponentInParent<BOSS_Script>().knockBackRotate = gameObject.transform.localScale.x;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("enemy"))
        {
            hit = true;
        }
    }
    public void shoot()
    {
        if (current_class == "ranged")
        {
            Instantiate(dard, attackPoint.transform.position, Quaternion.identity, this.transform);
        }
        else if (current_class == "mage")
        {
            Instantiate(N_dard, attackPoint.transform.position, Quaternion.identity, this.transform);
        }
    }
    public void branca()
    {
        if (current_class == "melee")
        {
            brancaGTomato.SetActive(true);
        }
        else if (current_class == "mage")
        {
            if (C_atk == "melee")
            {
                brancaNTomato.SetActive(true);
            }
        }
    }
    public IEnumerator Hit()
    {
        isDamaged = true;
        Current_HP--;
        anim.SetBool("isHit", true);
        rb.velocity = new Vector2(knockBack * knockBackRotate, rb.velocity.y);
        yield return new WaitForSeconds(0.05f);
        hit = false;
        anim.SetBool("isHit", false);
        isDamaged = false;
    }
    public IEnumerator Die()
    {
        anim.SetBool("isDead", true);
        gameOver.SetActive(true);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(5);
        SceneManager.LoadScene("Menu");
    }

    private void Run(float lerp)
    {
        float targetSpeed = horizontal * maxSpeed;
        targetSpeed = Mathf.Lerp(rb.velocity.x, targetSpeed, lerp);

        float accelRate;

        //Gets an acceleration value based on if we are accelerating (includes turning) 
        //or trying to decelerate (stop). As well as applying a multiplier if we're air borne.
        //if (LastOnGroundTime > 0)
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? runAccelAmount : runDeccelAmount;
        //else
        //    accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount * Data.accelInAir : Data.runDeccelAmount * Data.deccelInAir;
        if (Mathf.Abs(rb.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(rb.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && isGrounded == true)
		{
            //Prevent any deceleration from happening, or in other words conserve are current momentum
            //You could experiment with allowing for the player to slightly increae their speed whilst in this "state"
            accelRate = 0;
        }
        float speedDif = targetSpeed - rb.velocity.x;
        //Calculate force along x-axis to apply to thr player

        float movement = speedDif * accelRate;

        //Convert this to a vector and apply to rigidbody
        rb.AddForce(movement * Vector2.right, ForceMode2D.Force);

    }
}
