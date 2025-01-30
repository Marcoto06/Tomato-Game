using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class interactArea : MonoBehaviour
{
    public GameObject manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (gameObject.CompareTag("door") && collision.CompareTag("Player"))
        {
            if (Input.GetButtonDown("Jump"))
            {  
                Debug.Log("jajajsajdj");
                if (manager.GetComponent<roomManager>().floor == 0)
                {
                    SceneManager.LoadScene("Nv2");
                }
                else if (manager.GetComponent<roomManager>().floor == 1)
                {
                    SceneManager.LoadScene("Nv3");
                }
                else if (manager.GetComponent<roomManager>().floor == 2)
                {
                    SceneManager.LoadScene("Boss");
                }
            }
        }
    }
}

