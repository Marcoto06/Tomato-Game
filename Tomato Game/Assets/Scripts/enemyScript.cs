using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class enemyScript : MonoBehaviour
{
    public string EN_type;
    public int EN_HP;
    public float moveSpeed;
    public bool attacking;
    public bool player_in_range;
    public float timer;
    public int rotateValue;
    public bool RUN;
    public GameObject player;
    public Transform playerTransform;
    public SpriteRenderer mySprite;
    public Sprite[] enemySprites;
    public GameObject[] myHitbox;
    public Rigidbody2D myRb;
    public GameObject arrow;
    public Transform bow;
    public Transform[] pinyaPatrolPoints;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
        player_in_range = false;
        timer = 0;
        if (EN_type == "figa")
        {
            EN_HP = 100; moveSpeed = 3f;
            mySprite.sprite = enemySprites[0];
        }
        else if (EN_type == "pinya")
        {
            EN_HP = 350;
            mySprite.sprite = enemySprites[1];
        }
        else if (EN_type == "llimona")
        {
            EN_HP = 250;
            mySprite.sprite = enemySprites[2];
        }
    }

    // Update is called once per frame
    void Update()
    {
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
                    myRb.velocity = new Vector2(moveSpeed * -rotateValue, 0);
                }
                else if (playerTransform.position.x < gameObject.transform.position.x && playerTransform.localScale.x < 0)
                {
                    myRb.velocity = new Vector2(moveSpeed * rotateValue, 0);
                }
                else if (playerTransform.position.x > gameObject.transform.position.x && playerTransform.localScale.x < 0)
                {
                    myRb.velocity = new Vector2(moveSpeed * rotateValue, 0);
                }
                else
                {
                    myRb.velocity = new Vector2(moveSpeed * -rotateValue, 0);
                }
            }
        }
        else if (EN_type == "pinya")
        {
            //Adaptar el prefab a l'enemic seleccionat
            myHitbox[0].SetActive(false);
            myHitbox[1].SetActive(false);
            myHitbox[2].SetActive(true);
            if (playerTransform.position.x < gameObject.transform.position.x && playerTransform.localScale.x > 0)
            {
                rotateValue = -1;
            }
            else if (playerTransform.position.x < gameObject.transform.position.x && playerTransform.localScale.x < 0)
            {
                rotateValue = -1;
            }
            else if (playerTransform.position.x > gameObject.transform.position.x && playerTransform.localScale.x < 0)
            {
                rotateValue = 1;
            }
            else
            {
                rotateValue = 1;
            }
            gameObject.transform.localScale = new Vector3(0.09f * rotateValue, 0.09f, 0);

            //Atacar en proximitat del player
        }
        else if (EN_type == "llimona")
        {
            //Adaptar el prefab a l'enemic seleccionat
            myHitbox[0].SetActive(false);
            myHitbox[1].SetActive(true);
            myHitbox[2].SetActive(false);
            gameObject.transform.localScale = playerTransform.localScale;
        }
    }
    void shoot()
    {
        Instantiate(arrow, new Vector3 (bow.position.x, bow.position.y + 0.5f, bow.position.z), Quaternion.identity, this.gameObject.transform);
    }
}