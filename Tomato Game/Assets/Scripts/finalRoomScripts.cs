using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalRoomScripts : MonoBehaviour
{
    public SpriteRenderer portaSprite;
    public Sprite openDoor;
    public GameObject areaDoor;
    public GameObject manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController");
        if (manager.GetComponent<roomManager>().floor == 2)
        {
            OpenDoor();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor()
    {
        portaSprite.sprite = openDoor;
        areaDoor.SetActive(true);
    }
}
