using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour
{
    public GameObject followObject;
    public Transform followTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
