using System.Collections;
using System.Collections.Generic;
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
        transform.parent = null;
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        if (player != null)
        {
            Vector3 vectorToTarget = player.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = q;
            direction = player.transform.position - transform.position;
            rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") | collision.CompareTag("ground"))
        {
            collision.GetComponentInParent<movment>().hit = true;
            Destroy(gameObject);
        }
        else
        {
            return;
        }
    }
}
