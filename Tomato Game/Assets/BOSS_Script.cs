using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class BOSS_Script : MonoBehaviour
{
    public string EN_type;
    public float EN_MHP;
    public float EN_CHP;
    public float moveSpeed;
    public bool attacking;
    public bool player_in_range;
    public float timer;
    public int rotateValue;
    public bool RUN;
    public int patrolFlip;
    public float chargeForce;
    public GameObject player;
    public Transform playerTransform;
    public SpriteRenderer mySprite;
    public Sprite[] enemySprites;
    public GameObject[] myHitbox;
    public Rigidbody2D myRb;
    public GameObject arrow;
    public Transform bow;
    public Transform[] pinyaPatrolPointsSpawners;
    public Transform[] llimonaPatrolPointsSpawners;
    public GameObject limitPatrol;
    public GameObject[] PatrolPoints;
    public Animator anim;
    public LayerMask players;
    public GameObject attackPoint;
    public float radius;
    public bool hit;
    public float knockBackRotate;
    public int knockBack;
    public bool isBoss;
    Vector3 m_YAxis;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
        player_in_range = false;
        timer = 0;
        moveSpeed = 2f;
        rotateValue = 1;
        EN_MHP = 500;
        moveSpeed = 3.5f;
        gameObject.transform.localScale = new Vector3(0.15f * -rotateValue, 0.15f, 0);
        EN_CHP = EN_MHP;
    }

    // Update is called once per frame
    void Update()
    {
        myRb.rotation = 0f;

        if (hit == true)
        {
            StartCoroutine(Hit());
        }

        Vector3 scale = gameObject.transform.localScale;
        StartCoroutine(ChargePinya());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("enemy") | collision.collider.CompareTag("wall") | collision.collider.CompareTag("door") | collision.collider.CompareTag("finalWall"))
        {
            rotateValue *= -1;
        }
        else if (collision.collider.CompareTag("Player"))
        {
            StartCoroutine(PlayerHit());
        }
    }

    public IEnumerator ChargePinya()
    {
        anim.SetBool("P_isCharging", true);
        myRb.constraints = RigidbodyConstraints2D.FreezePosition;
        yield return new WaitForSeconds(2);
        anim.SetBool("P_isCharging", false);
        anim.SetBool("P_isAttacking", true);
        myRb.constraints = RigidbodyConstraints2D.None;
        myRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        myRb.velocity = new Vector2(moveSpeed * rotateValue * chargeForce, myRb.velocity.y);
        yield return new WaitForSeconds(1f);
        anim.SetBool("P_isAttacking", false);
        player_in_range = false;
    }
    public IEnumerator PlayerHit()
    {
        myRb.constraints = RigidbodyConstraints2D.FreezePosition;
        yield return new WaitForSeconds(3f);
        myRb.constraints = RigidbodyConstraints2D.None;
    }
    public IEnumerator Hit()
    {
        if (EN_CHP <= 0)
        {
            StartCoroutine(Die());
        }
        anim.SetBool("isHit", true);
        myRb.velocity = new Vector2(knockBack * knockBackRotate, myRb.velocity.y);
        yield return new WaitForSeconds(0.05f);
        hit = false;
        anim.SetBool("isHit", false);
    }

    public IEnumerator Die()
    {
        anim.SetBool("isDead", true);
        yield return new WaitForSeconds(0.4333333342f);
        Destroy(gameObject);
    }
}
