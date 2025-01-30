using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fonsScript : MonoBehaviour
{
    public GameObject manager;
    public SpriteRenderer mySprite;
    // Start is called before the first frame update
    void Start()
    {
        mySprite = gameObject.GetComponent<SpriteRenderer>();
        manager = GameObject.FindGameObjectWithTag("manager");
        if (manager.GetComponent<runManager>().ultimFons == 0)
        {
            mySprite.sprite = manager.GetComponent<runManager>().fons[1];
            manager.GetComponent<runManager>().ultimFons = 1;
        }
        else
        {
            mySprite.sprite = manager.GetComponent<runManager>().fons[0];
            manager.GetComponent<runManager>().ultimFons = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
