using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyVisionScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.GetComponentInParent<enemyScript>().player_in_range = true;
            if (gameObject.CompareTag("RUN"))
            {
                gameObject.GetComponentInParent<enemyScript>().RUN = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.GetComponentInParent<enemyScript>().player_in_range = false;
            gameObject.GetComponentInParent<enemyScript>().pinyaPatrolCreate();
            gameObject.GetComponentInParent<enemyScript>().llimonaPatrolCreate();
        }
        if (gameObject.CompareTag("RUN"))
        {
            gameObject.GetComponentInParent<enemyScript>().RUN = false;
        }
    }
}
