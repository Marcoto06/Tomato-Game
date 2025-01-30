using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2_Script : MonoBehaviour
{
    public GameObject room;
    public GameObject boss;
    public GameObject myBoss;
    // Start is called before the first frame update
    void Start()
    {
        myBoss = boss;
    }

    // Update is called once per frame
    void Update()
    {
      if (boss != myBoss)
        {
            room.GetComponent<finalRoomScripts>().OpenDoor();
        }
    }
}
