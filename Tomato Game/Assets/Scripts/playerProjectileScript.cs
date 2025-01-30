using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerProjectileScript : MonoBehaviour
{
    public GameObject player;
    public float timer;
    public Rigidbody2D rb;
    public float force;
    public bool shoot;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.parent = null;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(1*player.transform.localScale.x, 0).normalized * force;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            collision.GetComponentInParent<enemyScript>().EN_CHP -= player.GetComponent<movment>().PJ_DAM;
            collision.GetComponentInParent<enemyScript>().hit = true;
            Destroy(gameObject);
        }
        else if (collision.CompareTag("ground"))
        {
            Destroy(gameObject);
        }
    }
}
