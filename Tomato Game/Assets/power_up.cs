using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class power_up : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
            if (Random.value < 0.5f) // 50% chance
            {
                gameObject.tag = "powerUp_H";
            }
            else
            {
                gameObject.tag = "powerUp_D";
            }
        }

    // Update is called once per frame
    void Update()
    {

    }
    
}
