using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomManager : MonoBehaviour
{
    public int num_rooms = 1;
    public bool created_safeRoom;
    public bool created_upRoom;
    void Start()
    {
        num_rooms = 1;
        created_upRoom = false;
        created_safeRoom = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
