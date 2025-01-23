using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomGenerate : MonoBehaviour
{
    public GameObject manager;
    public GameObject[] L1Rooms;
    private int rand;
    // Start is called before the first frame update
    void Start()
    {
        rand = Random.Range(0, L1Rooms.Length);
        if (manager.GetComponent<roomManager>().num_rooms < 5)
        {
            Instantiate(L1Rooms[rand], transform.position, L1Rooms[rand].transform.rotation);
            manager.GetComponent<roomManager>().num_rooms++;
        } else { manager.GetComponent<roomManager>().num_rooms = 1; }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
