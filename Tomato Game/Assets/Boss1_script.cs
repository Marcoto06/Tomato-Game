using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_script : MonoBehaviour
{
    public GameObject[] figues;
    public GameObject room;
    int deadF;
    // Start is called before the first frame update
    void Start()
    {
        deadF = 0;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject i in figues)
        {
            if (i == null)
            {
                deadF++;
            }
        }
        if (deadF == 5)
        {
            room.GetComponent<finalRoomScripts>().OpenDoor();
        }
        else
        {
            deadF = 0;
        }
    }
}
