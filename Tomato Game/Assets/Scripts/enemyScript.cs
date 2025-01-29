using JetBrains.Annotations;
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
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        EN_CHP = EN_MHP;

        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
        player_in_range = false;
        timer = 0;
        if (EN_type == "figa")
        {
            moveSpeed = 3f;
             moveSpeed = 3f;
            mySprite.sprite = enemySprites[0];
        }
        else if (EN_type == "pinya")
        {
            moveSpeed = 2f;
            rotateValue = 1;
            myHitbox[0].SetActive(false);
            myHitbox[1].SetActive(true);
            myHitbox[2].SetActive(false);
            gameObject.transform.localScale = new Vector3(0.09f * rotateValue, 0.09f, 0);
            pinyaPatrolCreate();
            mySprite.sprite = enemySprites[1];
        }
        else if (EN_type == "llimona")
        {
            moveSpeed = 2.5f;
            rotateValue = 1;
            myHitbox[0].SetActive(false);
            myHitbox[1].SetActive(true);
            myHitbox[2].SetActive(false);
            gameObject.transform.localScale = new Vector3(0.15f * -rotateValue, 0.15f, 0);
            llimonaPatrolCreate();
            mySprite.sprite = enemySprites[2];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(EN_CHP<EN_MHP)
        {
            anim.SetTrigger("Attacked");
        }

        if (EN_CHP<=0)
        {
            anim.SetBool("isDead", true);
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
            if (player_in_range == true && RUN == false)
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
            else if (RUN == true)
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
                myRb.velocity = new Vector2(moveSpeed * rotateValue, myRb.velocity.y);
            } else if (!attacking)
            {
                Destroy(PatrolPoints[0]);
                Destroy(PatrolPoints[1]);
                myRb.velocity = new Vector2(moveSpeed * rotateValue * chargeForce, myRb.velocity.y);
            }
        }
        else if (EN_type == "llimona")
        {
            //Adaptar el prefab a l'enemic seleccionat
            myHitbox[0].SetActive(false);
            myHitbox[1].SetActive(false);
            myHitbox[2].SetActive(true);
            gameObject.transform.localScale = new Vector3(0.15f * -rotateValue, 0.15f, 0);
            //Atacar en proximitat del player
            if (!attacking)
            {
                myRb.velocity = new Vector2(moveSpeed * rotateValue, myRb.velocity.y);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (EN_type == "pinya" && collision.CompareTag("RUN"))
        {
            if (collision.gameObject == PatrolPoints[0] | collision.gameObject == PatrolPoints[1])
            {
                rotateValue *= -1;
            }
        } else if (EN_type == "llimona" && collision.CompareTag("RUN"))
        {
            if (collision.gameObject == PatrolPoints[0] | collision.gameObject == PatrolPoints[1])
            {
                rotateValue *= -1;
            }
        }
    }
}