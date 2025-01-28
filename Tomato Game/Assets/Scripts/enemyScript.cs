using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class enemyScript : MonoBehaviour
{
    public string EN_type;
    public float EN_HP;
    public float moveSpeed;
    public bool attacking;
    public bool player_in_range;
    public float timer;
    public int rotateValue;
    public GameObject player;
    public Transform playerTransform;
    public SpriteRenderer mySprite;
    public Sprite[] enemySprites;
    public GameObject[] myHitbox;
    public Rigidbody2D myRb;
    public GameObject arrow;
    public Transform bow;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
        player_in_range = false;
        timer = 0;
        if (EN_type == "figa")
        {
            EN_HP = 1; moveSpeed = 1;
        }
        else if (EN_type == "pinya")
        {
            EN_HP = 5;
        }
        else if (EN_type == "llimona")
        {
            EN_HP = 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 scale = gameObject.transform.localScale;
        if (EN_type == "figa")
        {
            //Adaptar el prefab a l'enemic seleccionat
            mySprite.sprite = enemySprites[0];
            myHitbox[0].SetActive(true);
            myHitbox[1].SetActive(false);
            myHitbox[2].SetActive(false);
            if (playerTransform.position.x < gameObject.transform.position.x && playerTransform.localScale.x > 0)
            {
                rotateValue = -1;
            } else if (playerTransform.position.x < gameObject.transform.position.x && playerTransform.localScale.x < 0)
            {
                rotateValue = 1;
            }
            else if (playerTransform.position.x > gameObject.transform.position.x && playerTransform.localScale.x < 0)
            {
                rotateValue = -1;
            } else { rotateValue = 1; }
            gameObject.transform.localScale = new Vector3(playerTransform.localScale.x * rotateValue, playerTransform.localScale.y, playerTransform.localScale.z);

            //Actuar nomes en proximitat del player
            if (player_in_range)
            {
                timer += Time.deltaTime;

                if (timer > 3)
                {
                    timer = 0;
                    shoot();
                }
            }
        }
        else if (EN_type == "pinya")
        {
            //Adaptar el prefab a l'enemic seleccionat
            mySprite.sprite = enemySprites[1];
            myHitbox[0].SetActive(false);
            myHitbox[1].SetActive(false);
            myHitbox[2].SetActive(true);
            gameObject.transform.localScale = new Vector3(playerTransform.localScale.x - 0.04f, playerTransform.localScale.y - 0.04f, 0);
        }
        else if (EN_type == "llimona")
        {
            //Adaptar el prefab a l'enemic seleccionat
            mySprite.sprite = enemySprites[2];
            myHitbox[0].SetActive(false);
            myHitbox[1].SetActive(true);
            myHitbox[2].SetActive(false);
            gameObject.transform.localScale = playerTransform.localScale;
        }
    }
    void shoot()
    {
        Instantiate(arrow, bow.position, Quaternion.identity, this.gameObject.transform);
    }
}
