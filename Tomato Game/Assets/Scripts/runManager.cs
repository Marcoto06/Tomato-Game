using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class runManager : MonoBehaviour
{
    public bool paused = false;
    public GameObject pauseMenu;
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                paused = false;
                pauseMenu.SetActive(false);
            }
            else
            {
                paused = true;
                pauseMenu.SetActive(true);
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
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
}
