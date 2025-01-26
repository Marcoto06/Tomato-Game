using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;

public class runManager : MonoBehaviour
{
    public bool paused = false;
    public GameObject pauseMenu;
    public GameObject manager;
    public GameObject eventSystem;
    public GameObject resumeButton;
    public int ranged_lvl;
    public int melee_lvl;
    public int mage_lvl;
    // Start is called before the first frame update
    void Start()
    {
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Options"))
        {
            if (paused)
            {
                paused = false;
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(resumeButton);
                paused = true;
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                paused = false;
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(null);
                paused = true;
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1  ;
    }

    public void ToMenu()
    {
        saveData data = new saveData();
        data.ranged_lvl = ranged_lvl;
        data.melee_lvl = melee_lvl ;
        data.mage_lvl = mage_lvl;
        string json = JsonUtility.ToJson(data);
        string path = Application.persistentDataPath + "/saveData.son";
        System.IO.File.WriteAllText(path, json);
        SceneManager.LoadScene("Menu");
    }
    public void nextFloor()
    {
        if (manager.GetComponent<roomManager>().floor == 0) {
            SceneManager.LoadScene("Nv2");
        }
        else if (manager.GetComponent<roomManager>().floor == 1)
        {
            SceneManager.LoadScene("Nv3");
        }
        else
        {
            SceneManager.LoadScene("Marc");
        }
    }
}
