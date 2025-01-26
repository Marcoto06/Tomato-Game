using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomGenerate : MonoBehaviour
{
    public GameObject manager;
    public GameObject[] Rooms;
    public GameObject finalRoom;
    public GameObject safeRoom;
    public GameObject upRoom;
    private int rand;
    // Start is called before the first frame update
    void Start()
    {
        rand = Random.Range(0, Rooms.Length);
        if (manager.GetComponent<roomManager>().num_rooms < 8)
        {   
            if (Rooms[rand].tag == "upRoom")
            {
                if (manager.GetComponent<roomManager>().created_upRoom == false)
                {
                    Instantiate(Rooms[rand], transform.position, Rooms[rand].transform.rotation);
                    manager.GetComponent<roomManager>().created_upRoom = true;
                } else { Instantiate(Rooms[0], transform.position, Rooms[0].transform.rotation); }
            }
            else if (Rooms[rand].tag == "safeRoom")
            {
                if (manager.GetComponent<roomManager>().created_safeRoom == false)
                {
                    Instantiate(Rooms[rand], transform.position, Rooms[rand].transform.rotation);
                    manager.GetComponent<roomManager>().created_safeRoom = true;
                } else { Instantiate(Rooms[0], transform.position, Rooms[0].transform.rotation); }
            }
            else {
                Instantiate(Rooms[rand], transform.position, Rooms[rand].transform.rotation); 
            }
            manager.GetComponent<roomManager>().num_rooms++;
        }
        else if (manager.GetComponent<roomManager>().created_safeRoom == false)
        {
            Instantiate(safeRoom, transform.position, safeRoom.transform.rotation);
            manager.GetComponent<roomManager>().created_safeRoom = true;
        }
        else if (manager.GetComponent<roomManager>().created_upRoom == false)
        {
            Instantiate(upRoom, transform.position, upRoom.transform.rotation);
            manager.GetComponent<roomManager>().created_upRoom = true;
        }
        else { Instantiate(finalRoom, transform.position, finalRoom.transform.rotation); manager.GetComponent<roomManager>().num_rooms = 1; manager.GetComponent<roomManager>().created_upRoom = false; manager.GetComponent<roomManager>().created_safeRoom = false; }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
