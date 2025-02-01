using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;

public class interactArea : MonoBehaviour
{
    public GameObject manager;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController");
        Player = GameObject.FindGameObjectWithTag("Player");
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
                saveData data = new saveData();
                data.floor = 0;
                data.current_HP = Player.GetComponent<movment>().Current_HP;
                data.player_class = Player.GetComponent<movment>().current_class;
                string json = JsonUtility.ToJson(data);
                string path = Application.persistentDataPath + "/saveData.json";
                System.IO.File.WriteAllText(path, json);
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

