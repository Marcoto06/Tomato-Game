using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour
{
    public GameObject followObject;
    public GameObject finalRoom;
    public GameObject Player;
    public GameObject finalWall;
    public Transform followTransform;
    public bool final;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        if (final != true)
        {
            followObject = Player;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (final != true)
        {
            if (finalRoom == null)
            {
                finalRoom = GameObject.FindGameObjectWithTag("finalRoom");
                finalWall = GameObject.FindGameObjectWithTag("finalWall");
                finalWall.SetActive(false);
            }
            if (Player.transform.position.x > finalRoom.transform.position.x)
            {
                followObject = finalRoom;
                finalWall.SetActive(true);
            }
        }
        followTransform = followObject.GetComponent<Transform>();
        if (followTransform.position.x < 0)
        {
            gameObject.transform.position = new Vector3(0, 0, -10);
        }
        else
        {
            gameObject.transform.position = new Vector3(followTransform.position.x, 0, -10);
        }
    }
}
