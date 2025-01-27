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
    public int HP;
    public float moveSpeed;
    public bool attacking;
    public GameObject player;
    public Transform playerTransform;
    public SpriteRenderer mySprite;
    public Sprite[] enemySprites;
    public GameObject[] myHitbox;
    public Rigidbody2D myRb;
    public bool player_in_range;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = player.GetComponent<Transform>();
        player_in_range = false;
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
            gameObject.transform.localScale = playerTransform.localScale;
            //Actuar nomes en proximitat del player
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player_in_range = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player_in_range = false;
            Debug.Log("?");
        }
    }
}
