using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrolPointScrpit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.GetComponentInParent<enemyScript>().PatrolPoints[0] == null)
        {
            gameObject.GetComponentInParent<enemyScript>().PatrolPoints[0] = this.gameObject;
        }
        else
        {
            gameObject.GetComponentInParent<enemyScript>().PatrolPoints[1] = this.gameObject;
        }
        gameObject.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
