using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class enemyScript : MonoBehaviour
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
        if (EN_type == "figa")
        {
            EN_MHP = 75;
            moveSpeed = 3f;
             moveSpeed = 3f;
            mySprite.sprite = enemySprites[0];
            anim.SetBool("isFiga", true);
        }
        else if (EN_type == "pinya")
        {
            EN_MHP = 200;
            moveSpeed = 2f;
            rotateValue = 1;
            myHitbox[0].SetActive(false);
            myHitbox[1].SetActive(true);
            myHitbox[2].SetActive(false);
            gameObject.transform.localScale = new Vector3(0.09f * rotateValue, 0.09f, 0);
            pinyaPatrolCreate();
            mySprite.sprite = enemySprites[1];
            anim.SetBool("isPinya", true);
        }
        else if (EN_type == "llimona")
        {
            EN_MHP = 100;
            moveSpeed = 2.5f;
            rotateValue = 1;
            myHitbox[0].SetActive(false);
            myHitbox[1].SetActive(true);
            myHitbox[2].SetActive(false);
            gameObject.transform.localScale = new Vector3(0.15f * -rotateValue, 0.15f, 0);
            llimonaPatrolCreate();
            mySprite.sprite = enemySprites[2];
            anim.SetBool("isLlimona", true);
        }
        if (isBoss == true)
        {
            EN_MHP = 500;
            moveSpeed = 3.5f;
            gameObject.transform.localScale = new Vector3(0.2f * -rotateValue, 0.2f, 0);
            anim.SetBool("isBoss", true);
        }

        EN_CHP = EN_MHP;
    }

    // Update is called once per frame
    void Update()
    {
        myRb.rotation = 0f;

        if(hit == true)
        {
            StartCoroutine(Hit());
        }

        Vector3 scale = gameObject.transform.localScale;
        if (EN_type == "figa")
        {
            //Adaptar el prefab a l'enemic seleccionat
            myHitbox[0].SetActive(true);
            myHitbox[1].SetActive(false);
            myHitbox[2].SetActive(false);
            Vector3 vectorToTarget = player.transform.position - bow.position;
            float angle; 
            if (playerTransform.position.x < gameObject.transform.position.x && playerTransform.localScale.x > 0)
            {
                rotateValue = -1;
                angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg + 180;
            } else if (playerTransform.position.x < gameObject.transform.position.x && playerTransform.localScale.x < 0)
            {
                rotateValue = 1;
                angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg + 180;
            }
            else if (playerTransform.position.x > gameObject.transform.position.x && playerTransform.localScale.x < 0)
            {
                rotateValue = -1;
                angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            } else 
            { 
                rotateValue = 1; 
                angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg; 
            }
            gameObject.transform.localScale = new Vector3(playerTransform.localScale.x * rotateValue, playerTransform.localScale.y, playerTransform.localScale.z);

            //Actuar nomes en proximitat del player
            if (player_in_range == true)
            {
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                bow.rotation = q;
                timer += Time.deltaTime;

                if (timer > 3)
                {
                    timer = 0;
                    shoot();
                }
            }
            if (RUN == true)
            {
                if (playerTransform.position.x < gameObject.transform.position.x && playerTransform.localScale.x > 0)
                {
                    myRb.velocity = new Vector2(moveSpeed * -rotateValue, myRb.velocity.y);
                }
                else if (playerTransform.position.x < gameObject.transform.position.x && playerTransform.localScale.x < 0)
                {
                    myRb.velocity = new Vector2(moveSpeed * rotateValue, myRb.velocity.y);
                }
                else if (playerTransform.position.x > gameObject.transform.position.x && playerTransform.localScale.x < 0)
                {
                    myRb.velocity = new Vector2(moveSpeed * rotateValue, myRb.velocity.y);
                }
                else
                {
                    myRb.velocity = new Vector2(moveSpeed * -rotateValue, myRb.velocity.y);
                }
            }
        }
        else if (EN_type == "pinya")
        {
            //Adaptar el prefab a l'enemic seleccionat
            myHitbox[0].SetActive(false);
            myHitbox[1].SetActive(true);
            myHitbox[2].SetActive(false);
            gameObject.transform.localScale = new Vector3(0.09f * rotateValue, 0.09f, 0);

            //Atacar en proximitat del player
            if (!player_in_range)
            {
                anim.SetFloat("P_Walk_velocity", Math.Abs(myRb.velocity.x));
                myRb.velocity = new Vector2(moveSpeed * rotateValue, myRb.velocity.y);
                anim.SetBool("P_isAttacking", false);
            } else 
            {
                StartCoroutine(ChargePinya());
            }
        }
        else if (EN_type == "llimona")
        {
            //Adaptar el prefab a l'enemic seleccionat
            myHitbox[0].SetActive(false);
            myHitbox[1].SetActive(false);
            myHitbox[2].SetActive(true);
            if (isBoss != true)
            {
                gameObject.transform.localScale = new Vector3(0.15f * -rotateValue, 0.15f, 0);
            }
            else { gameObject.transform.localScale = new Vector3(0.2f * -rotateValue, 0.2f, 0); }
            anim.SetFloat("L_Walk_velocity", Math.Abs(myRb.velocity.x));
            //Atacar en proximitat del player
            if (attacking == false)
            {
                myRb.velocity = new Vector2(moveSpeed * rotateValue, myRb.velocity.y);
            }
            else
            {
                myRb.velocity = new Vector2(0, 0);
                StartCoroutine(AttackLlimona());
            }
            if (player_in_range)
            {
                Destroy(PatrolPoints[0]);
                Destroy(PatrolPoints[1]);
            }
        }
    }
    void shoot()
    {
        Instantiate(arrow, new Vector3 (bow.position.x, bow.position.y + 0.5f, bow.position.z), Quaternion.identity, this.gameObject.transform);
    }
    public void pinyaPatrolCreate()
    {
        if (PatrolPoints[0] == null && PatrolPoints[0] == null)
        {
            Instantiate(limitPatrol, pinyaPatrolPointsSpawners[0].position, transform.rotation, this.transform);
            Instantiate(limitPatrol, pinyaPatrolPointsSpawners[1].position, transform.rotation, this.transform);
        }
    }
    public void llimonaPatrolCreate()
    {
        if (PatrolPoints[0] == null && PatrolPoints[0] == null)
        {
            Instantiate(limitPatrol, llimonaPatrolPointsSpawners[0].position, transform.rotation, this.transform);
            Instantiate(limitPatrol, llimonaPatrolPointsSpawners[1].position, transform.rotation, this.transform);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (EN_type == "pinya" && collision.collider.CompareTag("RUN"))
        {
            if (collision.gameObject == PatrolPoints[0] | collision.gameObject == PatrolPoints[1])
            {
                rotateValue *= -1;
            }
        } else if (EN_type == "llimona" && collision.collider.CompareTag("RUN"))
        {
            if (collision.gameObject == PatrolPoints[0] | collision.gameObject == PatrolPoints[1])
            {
                rotateValue *= -1;
            }
        }
        if (collision.collider.CompareTag("enemy") | collision.collider.CompareTag("wall") | collision.collider.CompareTag("door") | collision.collider.CompareTag("finalWall"))
        {
            rotateValue *= -1;
        }
    }

    

    public IEnumerator AttackLlimona()
    {
        anim.SetBool("L_isAttacking", true);
        yield return new WaitForSeconds(0.8f);
        myRb.constraints = RigidbodyConstraints2D.FreezePosition;
        yield return new WaitForSeconds(0.8f);
        anim.SetBool("L_isAttacking", false);
        myRb.constraints = RigidbodyConstraints2D.None;
        myRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        attacking = false;
    }
    public IEnumerator ChargePinya()
    {
        Destroy(PatrolPoints[0]);
        Destroy(PatrolPoints[1]);
        anim.SetBool("P_isCharging", true);
        myRb.constraints = RigidbodyConstraints2D.FreezePosition;
        yield return new WaitForSeconds(2);
        anim.SetBool("P_isCharging", false);
        anim.SetBool("P_isAttacking", true);
        myRb.constraints = RigidbodyConstraints2D.None;
        myRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        myRb.velocity = new Vector2(moveSpeed * rotateValue * chargeForce, myRb.velocity.y);
        yield return new WaitForSeconds(0.5f);
        player_in_range = false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }

    public void L_Attack()
    {
        Collider2D[] playerCol = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, players);
        foreach (Collider2D playerGameobject in playerCol)
        {
            Debug.Log("Hit player");
            playerGameobject.GetComponent<movment>().knockBackRotate = rotateValue;
            playerGameobject.GetComponent<movment>().hit = true;
        }
    }

    public IEnumerator Hit()
    {
        if (EN_CHP <= 0)
        {
            StartCoroutine(Die());
        }
        anim.SetBool("isHit", true);
        if (player.transform.position.x < transform.position.x)
        {
            knockBackRotate = 1;
        }
        else { knockBackRotate = -1; }
        myRb.velocity = new Vector2(knockBack * knockBackRotate, myRb.velocity.y);
        yield return new WaitForSeconds(0.05f);
        hit = false;
        anim.SetBool("isHit", false);
    }

    public IEnumerator Die()
    {
        anim.SetBool("isDead", true);
        if(isBoss == true)
        {
            gameObject.GetComponentInParent<finalRoomScripts>().OpenDoor();
        }
        yield return new WaitForSeconds(0.4333333342f);
        Destroy(gameObject);
    }
}