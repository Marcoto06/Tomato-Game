using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class arrowScript : MonoBehaviour
{
    public GameObject player;
    public float timer;
    public Rigidbody2D rb;
    Vector3 direction;
    public float force;
    public bool shoot;
    public Transform bow;
    Vector3 initialpos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        initialpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        direction = player.transform.position - transform.position;
        timer += Time.deltaTime;

        if (timer > 3 && shoot == false)
        {
            rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
            shoot = true;
            transform.parent = null;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
