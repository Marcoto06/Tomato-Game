using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomManager : MonoBehaviour
{
    public int num_rooms = 1;
    public bool created_safeRoom;
    public bool created_upRoom;
    //private int rand;
    //public int floor_rooms;
    // Start is called before the first frame update
    void Start()
    {
        num_rooms = 1;
        //rand = Random.Range(7, 10);
        //floor_rooms = rand;
        created_upRoom = false;
        created_safeRoom = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
